using DataBase.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.System;

namespace DataBase
{
    public static class Server
    {
        private static string dbPath = ApplicationData.Current.LocalFolder.Path;
        private static string connectionString = "Filename=" + dbPath + "\\DBGame.db";
        /*
 הפעולה בודקת אם המשתמש הזין נתונים נכונים ונמצא במאגר משתמשים
 של המשתמש UserId אם הכל תקין, הפעולה מחזירה את  
 null אם הנתונים אינם תקינים הפעולה מחזירה ערך x
*/
        public static int? ValidateUser(string userName, string userPassword)
        {
            string query = $"SELECT UserId FROM [User] WHERE UserName='{userName}' AND UserPassword='{userPassword}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
                return null;
            }
        }
        /*
הפעולה בודקת אם המשתמש הזין נתונים נכונים ונמצא במאגר משתמשים
של המשתמש UserId אם הכל תקין, הפעולה מחזירה את  
null אם הנתונים אינם תקינים הפעולה מחזירה ערך x
*/
        public static int? ValidateUserMail(string mail)
        {
            string query = $"SELECT UserId FROM [User] WHERE UserMail='{mail}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
                return null;
            }
        }
        /*
        הפעולה בודקת אם המשתמש הזין נתונים נכונים ונמצא במאגר משתמשים
 של המשתמש UserId אם הכל תקין, הפעולה מחזירה את  
 null אם הנתונים אינם תקינים הפעולה מחזירה ערך x
*/
        public static int? ValidateUserName(string userName)
        {
            string query = $"SELECT UserId FROM [User] WHERE UserName='{userName}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
                return null;
            }
        }
        //        הפעולה מוסיפה משתמש חדש למסד הנתונים.בפועל הפעולה מוסיפה נתונים ל- 3 טבלאות
        //GameData יתווספו הנתונים האישיים של המשתמש.לטבלת User לטבלת.User, GameData, UserProduct :שהם
        //יתווספו נתוני ברירת מחדל של המשחק מפני שהמשתמש משחק בפעם הראשונה

        //Feature שהוא בעצם המחסן המשותף תתווסף שורה המציינת שהשחקן החדש מקבל בחינם UserProduct לטבלת

        //ברירת מחדל

        //חשוב להדגיש: הפעולה מחזירה עצם משתמש שהוא מלא בנתונים ומוכן לשחק
        //*/
        public static GameUser AddNewUser(string name, string password, string mail)
        {
            int? userId = ValidateUser(name, password); // בדיקה אם המשתמש כבר נמצא במאגר
            if (userId != null) // המשתמש כבר קיים - לשלוח להתחברות במקום הרשמה
                return null;
            // אם המשכנו, זאת אומרת המשתמש בעל הנתונים שהזין לא נמצא במאגר
            //User מסיפים את נתוניו האישיים של המשתמש שהזין לטבלת 
            string query = $"INSERT INTO [User] (UserName,UserPassword,UserMail) VALUES ('{name}','{password}','{mail}')";
            Execute(query);
            userId = ValidateUser(name, password); //User של המשתמש לאחר הוספתו לטבלת UserId קבלת 
                                                   //-------------------------------------------
            AddGameData(userId.Value); //הוספת נתוני ברירת מחדל 
            AddUserProduct(userId.Value);
            return GetUser(userId.Value);
        }
        // פונקציה: Execute
        // תיאור: מבצעת שאילתת SQL על מסד הנתונים על ידי ביצוע הפעולה המצויינת בשאילתה.
        // פרמטרים:
        //   - query: השאילתה ה-SQL שתבוצע על מסד הנתונים.
        public static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        public static void AddProduct(int userId, int productId)
        {
            string query = $"INSERT INTO [UserProduct] (UserId, ProductId) VALUES ({userId}, {productId})";
            Execute(query);
        }
        /*
           הפעולה מחזירה משתמש אשר כל שדותיו מלאים
           הפעולה אוספת נתונים מ- 4 טבלאות וממלאה באמצעותם את המשתמש 
           ולוקחת משם User כדי שיוכל לגשת למשחק. בשלב התחלתי הפעולה ניגשת לטבלת
           של המשתמש Id,Name,Mail
           הממשיכה למלא את נתוני המשתמש,SetUser לאחר מכן הפעולה נעזרת בפעולת עזר 
         */
        public static GameUser GetUser(int userId)
        {
            GameUser user = null;
            string query = $"SELECT UserId, UserName, UserMail FROM [User] WHERE UserId={userId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new GameUser
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                    };
                }
            }
            if (user != null)
            {
                SetUser(user);//המשך מילוי משתמש
            }
            return user; // user doesnt exsit
        }
/*
        הפעולה ממשיכה למלא את שדותיו של המשתמש.בשלב הראשון
  MaxLevel,Money,CurrentLevelId,CurrentProductId :ושולפת משם את נתוני המשחק של המשתמש GameData היא ניגשת לטבלת
  נכנסים וממלאים משתמש MaxLevel,Money ,כמו כן
  במשתמש CurrentLevel -על מנת למלא את ה Level ניגשים לטבלת CurrentLevelId לאחר מכן באמצעות
  SetCurrentLevel על זה תהיה אחראית פעולת עזר
  GameData ששלפנו מהטבלה currentProductId בשלב הבא בעזרת
  אשר השחקן שיחק בפעם האחרונה Feature -כדי לשלוף ממנה את השם ה Product ניגשים לטבלה
  SetCurrentProduct על זה תהיה אחראית הפעולה.GameUser -הנתון הזה גם יכנס ל
  GameUser לסיכום, באופן מדורג נאספו הנתונים מארבע טבלאות ומילאו את העצם
  כעת יוכל המשתמש לגשת למשחק
  */
 private static void SetUser(GameUser user)
        {
            int currentLevelId = 0;
            string query = $"SELECT Points, CurrentLevelId, CurrentProduct FROM [GameData] WHERE UserId={user.Id}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user.Points = reader.GetInt32(0);
                    currentLevelId = reader.GetInt32(1);
                    user.UsingProduct = reader.GetString(2) ;
                }
            }
            SetCurrentLevel(user, currentLevelId);
            //SetCurrentProduct(user, currentProductId);
        }
        // פונקציה: SetCurrentProduct
        // תיאור: מגדירה את המוצר הנוכחי של משתמש במשחק על פי מזהה המוצר.
        // פרמטרים:
        //   - user: אובייקט ה-GameUser שבו יוגדר המוצר הנוכחי.
        //   - currentProductId: מזהה המוצר שבו יתווסף למשתמש כמוצר נוכחי.
        public static void SetCurrentProduct(GameUser user, int currentProductId)
        {
            string query = $"SELECT ProductName FROM [Product] WHERE ProductId={currentProductId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user.UsingProduct = reader.GetString(0);
                }
            }
        }
        /*
       ,שולפת ממנה את נתוני רמת הקושי currentLevelId ולפי Level הפעולה ניגשת לטבלת
         ומכניסה אותו לתוך המשתמש GameLevel בשלב הבא הפעולה בונה עצם מסוג
         מפני שנעזר בה בעקבות החלפת רמת הקושי public הדגש: הפעולה
        */
        public static void SetCurrentLevel(GameUser user, int currentLevelId)
        {
            string query = $"SELECT LevelId, BlockSpeed FROM [Level] WHERE LevelId={currentLevelId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user.Level = new GameLevel
                    {
                        Speed = (0)
                    };
                }
            }

        }

        // פונקציה: AddUserProduct
        // תיאור: מוסיפה רשומת מוצר למשתמש במסד הנתונים.
        // פרמטרים:
        //   - userId: מזהה המשתמש שלו יתווסף המוצר.
        //   - productId: מזהה המוצר שיתווסף למשתמש (ברירת מחדל היא 1).

        public static void AddUserProduct(int userId, int productId = 1)
        {
            string query = $"INSERT INTO [UserProduct] (UserId,ProductId) VALUES ({userId},{productId})";
            Execute(query);
        }
        // פונקציה: ChangePassword
        // תיאור: משנה את הסיסמה של משתמש במסד הנתונים.
        // פרמטרים:
        //   - username: שם המשתמש של מי שנרצה לשנות את הסיסמה שלו.
        //   - newpass: הסיסמה החדשה שתוחלף למשתמש.
        public static void ChangePassword(string username, string newpass)
        {
            string query = $"UPDATE [User] SET UserPassword = '{newpass}' WHERE UserName = '{username}'";
            Execute(query);
        }
        // פונקציה: AddGameData
        // תיאור: מוסיפה רשומת נתוני משחק חדשה למסד הנתונים עבור משתמש חדש.
        // פרמטרים:
        //   - userId: מזהה המשתמש שלו יוצרים את רשומת נתוני המשחק.
        private static void AddGameData(int userId)
        {
            string query = $"INSERT INTO [GameData] (UserId, CurrentProduct, Points, CurrentLevelId) VALUES ({userId},{1},{0},{1})";
            Execute(query);
        }
        //מחזיר אמת אם יש למשתמש את המוצר ושקר אם אין לו
        public static bool ValidateProduct(int userId, int productId)
        {
            string query = $"SELECT * FROM [UserProduct] WHERE UserId='{userId}' AND ProductId='{productId}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                return reader.HasRows;
                
                //if (reader.HasRows)
                //{
                //    reader.Read();
                //    return reader.GetBoolean(0);
                //}
                //return false;
            }
        }
        //מחזיר את מיספר המוצר לפי המחיר שלו
        public static int GetProductId(string productName)
        {
            string query = $"SELECT ProductId FROM [Product] WHERE ProductName='{productName}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
                return 0;
            }
        }
        // פונקציה: GetProductPrice
        // תיאור: מחזירה את מחיר המוצר ממסד הנתונים על פי שם המוצר.
        // פרמטרים:
        //   - name: שם המוצר שלו רוצים לקבל את המחיר.
        // מחזיר:
        //   - המחיר של המוצר אם נמצא, אחרת 0.
        public static int GetProductPrice(string name)
        {
            string query = $"SELECT ProductPrice FROM [Product] WHERE ProductName='{name}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
                return 0;
            }
        }
        // פונקציה: UpdateUser
        // תיאור: מעדכנת את פרטי המשתמש במסד הנתונים.
        // פרמטרים:
        //
        public static void UpdateUser(GameUser user)
        {
            string query = $"UPDATE GameData SET  CurrentProduct = '{user.UsingProduct}', Points = '{user.Points}' WHERE UserId = '{user.Id}'";
            Execute(query);
            
        }
        // פונקציה: UpdateScore
        // תיאור: מעדכנת את הניקוד (נקודות) של משתמש במשחק במסד הנתונים.
        // פרמטרים:
        //
        public static void UpdateScore(GameUser user)
        {
            string query = $"UPDATE GameData SET Points = '{user.Points}' WHERE UserId = '{user.Id}'";
            Execute(query);

        }
    }
}
