using DataBase.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataBase
{
    public static class Server
    {
        private static string dbPath = ApplicationData.Current.LocalFolder.Path;
        private static string connectionString = "Filename=" + dbPath + "\\DBGame.db";
        /*
 הפעולה בודקת אם המשתמש הזין נתונים נכונים ונמצא במאגר משתמשים
 של המשתמש UserId אם הכל תקין, הפעולה מחזירה את  
 null אם הנתונים אינם תקינים הפעולה מחזירה ערך 
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

        private static void Execute(string query)
        {
            throw new NotImplementedException();
        }

        private static GameUser GetUser(int value)
        {
            throw new NotImplementedException();
        }

        private static void AddUserProduct(int value)
        {
            throw new NotImplementedException();
        }

        private static void AddGameData(int value)
        {
            throw new NotImplementedException();
        }
    }
}
