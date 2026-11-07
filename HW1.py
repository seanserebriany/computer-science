def calculate_ticket_price(base_price, passenger_age, is_student):
    """
    calculates ticket price with discounts. takes only the highest discount.
    """
    price_after_discount = base_price

    if passenger_age < 5:
        price_after_discount = 0.0
    elif 5 <= passenger_age <= 17 or passenger_age >= 65:
        price_after_discount *= 0.5
    elif is_student and passenger_age >= 18:
        price_after_discount *= 0.8

    return round(price_after_discount, 1)


def is_direct_route(train_schedule, direction, start_station_index, destination_station_index):
    """
    checks if a train can travel directly from start station to destination station.
    """
    num_stations = len(train_schedule)

    # first check boundaries: make sure both indexes exist in the schedule
    if not (0 <= start_station_index < num_stations and 0 <= destination_station_index < num_stations):
        return False

    # start and destination cannot be the same station
    if start_station_index != destination_station_index:
        start_enter, start_leave = train_schedule[start_station_index]
        dest_enter, dest_leave = train_schedule[destination_station_index]

        # check if the train stops at both stations (not -1)
        if start_enter != -1 and start_leave != -1 and dest_enter != -1 and dest_leave != -1:
            # for ltr: start station index must come before destination station index
            if direction == "ltr" and start_station_index < destination_station_index:
                # arrival time at destination cannot be earlier than departure time at start
                if dest_enter >= start_leave:
                    return True
            # for rtl: start station index must be greater than destination station index
            elif direction == "rtl" and start_station_index > destination_station_index:
                # arrival time at destination cannot be earlier than departure time at start
                if dest_enter >= start_leave:
                    return True

    return False


def calculate_travel_time(train_schedule, direction, start_station_index, destination_station_index):
    """
    returns travel duration between two stations or -1 if direct trip is not possible.
    """
    # use is_direct_route to avoid code duplicate
    if is_direct_route(train_schedule, direction, start_station_index, destination_station_index):
        start_enter, start_leave = train_schedule[start_station_index]
        dest_enter, dest_leave = train_schedule[destination_station_index]
        # total travel time = arrival time at destination minus departure time from start
        return dest_enter - start_leave

    return -1


def initialize_station(schedules, station_index):
    """
    adds a new empty station (-1, -1) to all train schedules in place.
    """
    if station_index < 0:
        return

    # if index is invalid for any train, stop and do nothing
    for train_schedule in schedules:
        if station_index > len(train_schedule):
            return

    # add (-1, -1) to all train schedules at station_index
    for train_schedule in schedules:
        train_schedule.insert(station_index, (-1, -1))


def split_schedule(schedules, train_directions, train_index, split_station):
    """
    splits one train into two separate trains at split_station in place.
    """
    # check that train_index and split_station are in valid range
    if train_index < 0 or train_index >= len(schedules):
        return
    if split_station < 1 or split_station >= len(schedules[train_index]):
        return

    original_train = schedules[train_index]
    train_len = len(original_train)

    schedule1 = []
    schedule2 = []

    # build the two new schedules:
    # train 1 keeps stations from 0 up to split_station (exclusive), rest are (-1, -1)
    # train 2 keeps stations from split_station to the end, earlier stations are (-1, -1)
    for i in range(train_len):
        if i < split_station:
            schedule1.append(original_train[i])
            schedule2.append((-1, -1))
        else:
            schedule1.append((-1, -1))
            schedule2.append(original_train[i])

    # in-place update:
    # 1. overwrite the original train with the first part
    schedules[train_index] = schedule1
    # 2. insert the second part right next to it at train_index + 1
    schedules.insert(train_index + 1, schedule2)
    # 3. duplicate the original direction for the newly created train
    train_directions.insert(train_index + 1, train_directions[train_index])


def find_conflicts(schedules):
    """
    finds all conflicts between trains at the same station.
    """
    conflicts = []
    num_trains = len(schedules)

    if num_trains == 0:
        return conflicts

    num_stations = len(schedules[0])

    # loop order is strictly (train i -> train j -> station)
    # this guarantees the result list is sorted lexicographically by default
    for i in range(num_trains):
        for j in range(i + 1, num_trains):
            for station in range(num_stations):
                train_a = schedules[i][station]
                train_b = schedules[j][station]

                # conflict is only possible if both trains stop at this station
                if train_a != (-1, -1) and train_b != (-1, -1):
                    arr_a, dep_a = train_a
                    arr_b, dep_b = train_b

                    # check time overlap:
                    # train a arrives before b leaves and train b arrives before a leaves.
                    # using <= because touching times count as conflict.
                    if arr_a <= dep_b and arr_b <= dep_a:
                        conflicts.append((i, j, station))

    return conflicts


def delay_train(train_schedule, start_station, delay, direction='ltr'):
    """
    delays train schedule from start_station to the end of the trip in place.
    """
    station_num = len(train_schedule)

    # for 'ltr': the delay moves forward from start_station towards the end
    if direction == 'ltr':
        for i in range(start_station, station_num):
            x, y = train_schedule[i]
            # skip stations where the train doesn't stop
            if x != -1 and y != -1:
                train_schedule[i] = (x + delay, y + delay)

    # for 'rtl': the delay moves backward from start_station down to 0
    elif direction == 'rtl':
        # step is -1 to loop backwards
        for i in range(start_station, -1, -1):
            x, y = train_schedule[i]
            # skip stations where the train doesn't stop
            if x != -1 and y != -1:
                train_schedule[i] = (x + delay, y + delay)


def resolve_conflicts(schedules, train_directions, delay):
    """
    fixes conflicts step by step by delaying the first conflicting train.
    """
    counter = 0

    # repeat loop until find_conflicts returns an empty list
    while find_conflicts(schedules) != []:
        # find conflicts after the latest schedule updates
        conflicts = find_conflicts(schedules)

        # the list is already sorted lexicographically, so [0] is the highest priority
        first_conflict = conflicts[0]
        train_a, train_b, station = first_conflict

        # the requirement is to always delay train_a (the lower index train)
        # delay propagates from the conflict station forward based on its direction
        delay_train(schedules[train_a], station, delay, train_directions[train_a])
        counter += 1

    return counter


def find_direct_trains(company, start_station, destination_station, departure_window):
    """
    finds all direct trains that leave between earliest and latest departure times.
    """
    station_names = company['station_names']
    schedules = company['schedules']
    directions = company['train_directions']
    prices = company['base_ticket_prices']

    # find matrix column index for start and destination stations
    start_idx = station_names.index(start_station)
    dest_idx = station_names.index(destination_station)

    # unpack window limits: earliest and latest departure times
    earliest_dep, latest_dep = departure_window
    matching_trains = []

    for train_idx in range(len(schedules)):
        train_sched = schedules[train_idx]
        train_dir = directions[train_idx]

        # calculate_travel_time checks if direct route exists and gives duration in one go
        travel_time = calculate_travel_time(train_sched, train_dir, start_idx, dest_idx)

        # if travel_time != -1, a direct route exists
        if travel_time != -1:
            # departure time is the leave time from the start station
            dep_time = train_sched[start_idx][1]

            # check if train leaves inside requested time window (inclusive)
            if earliest_dep <= dep_time <= latest_dep:
                arr_time = train_sched[dest_idx][0]
                price = prices[train_idx]

                # append matching train info dictionary
                matching_trains.append({
                    'train_index': train_idx,
                    'departure_time': dep_time,
                    'arrival_time': arr_time,
                    'travel_time': travel_time,
                    'base_price': price
                })

    return matching_trains
    


if __name__ == "__main__":
    def test_initialize_station():
        schedules = [[(0, 5), (10, 15), (20, 25)], [(2, 7), (12, 17), (22, 27)]]
        initialize_station(schedules, 1)
        try:
            assert schedules[0] == [(0, 5), (-1, -1), (10, 15), (20, 25)]
            assert schedules[1] == [(2, 7), (-1, -1), (12, 17), (22, 27)]
            print("test_add_train_station: PASSED")
        except AssertionError:
            print("test_add_train_station: FAILED")



    def test_split_schedule():
        schedules = [[(0, 5), (10, 15), (20, 25)], [(2, 7), (12, 17), (22, 27)]]
        directions = ['ltr', 'rtl']
        split_schedule(schedules, directions, 0, 2)
        try:
            assert len(schedules) == 3
            assert len(directions) == 3
            assert schedules[0] == [(0, 5), (10, 15), (-1, -1)]
            assert schedules[1] == [(-1, -1), (-1, -1), (20, 25)]
            assert schedules[2] == [(2, 7), (12, 17), (22, 27)]
            assert directions[0] == 'ltr'
            assert directions[1] == 'ltr'
            assert directions[2] == 'rtl'
            print("test_split_schedule: PASSED")
        except AssertionError:
            print("test_split_schedule: FAILED")


    def test_find_conflicts():
        schedules = [[(0, 5), (10, 15), (20, 25)], [(2, 7), (12, 17), (22, 27)], [(0, 3), (16, 18), (30, 35)]]
        conflicts = find_conflicts(schedules)
        try:
            assert len(conflicts) == 6
            print("test_find_conflicts: PASSED")
        except AssertionError:
            print("test_find_conflicts: FAILED")


    def test_delay_train():
        schedules = [[(0, 5), (10, 15), (20, 25)], [(2, 7), (12, 17), (22, 27)]]
        delay_train(schedules[1], 1, 5, direction='ltr')
        delay_train(schedules[1], 2, 3, direction='rtl')
        try:
            assert schedules[1] == [(5, 10), (20, 25), (30, 35)]
            print("test_delay_train: PASSED")
        except AssertionError:
            print("test_delay_train: FAILED")


    def test_resolve_conflicts():
        schedules = [[(0, 5), (10, 15)], [(3, 8), (12, 17)]]
        train_directions = ['ltr', 'ltr']
        schedules = [[(0, 5), (10, 15), (20, 25)], [(22, 27), (12, 17), (2, 7)], [(30, 35), (16, 18), (0, 3)]]
        train_directions = ['ltr', 'rtl', 'rtl']

        total_fixed = resolve_conflicts(schedules, train_directions, delay=5)

        try:
            assert total_fixed > 0
            assert len(find_conflicts(schedules)) == 0
            print("test_resolve_conflicts: PASSED")
        except AssertionError:

            print("test_resolve_conflicts: FAILED")


    # Run all tests
    if __name__ == "__main__":
        test_initialize_station()
        test_split_schedule()
        test_find_conflicts()
        test_delay_train()
        test_resolve_conflicts()
