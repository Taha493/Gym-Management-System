CREATE TABLE gym_member (
    member_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(MAX),
    last_name NVARCHAR(MAX),
    username NVARCHAR(100) UNIQUE,
    user_password NVARCHAR(MAX),
    gender NVARCHAR(MAX),
    dob DATE,
    phone_number NVARCHAR(MAX),
    email_address NVARCHAR(MAX),
    user_address NVARCHAR(MAX)
);


CREATE TABLE Trainer (
    trainer_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(20),
    last_name NVARCHAR(20),
    username NVARCHAR(20) UNIQUE,
    password NVARCHAR(20),
    gender NVARCHAR(10),
    DOB DATE,
    phone_number NUMERIC(20),
    email NVARCHAR(30),
    address NVARCHAR(50),
	qualifications NVARCHAR(100),
	specialty_areas NVARCHAR(100),
	experience NVARCHAR(50)
);

CREATE TABLE Personal_Sessions (
    SessionID INT IDENTITY(1,1) PRIMARY KEY,
    MemberID INT NOT NULL,
    TrainerID INT NOT NULL,
    Date_and_Time DATETIME,
    Status NVARCHAR(20) DEFAULT 'Pending', -- Accepted, Rejected, Pending, Rescheduled
    FOREIGN KEY (MemberID) REFERENCES gym_member(member_id),
    FOREIGN KEY (TrainerID) REFERENCES Trainer(trainer_id)
);

--drop table Personal_Sessions

--drop table WorkoutPlans
--drop table FitnessGoals
--drop table MusclesTargeted
--drop table Exercises
--drop table PlanDetails

-- Table: Fitness Goals
CREATE TABLE FitnessGoals (
    goal_id INT IDENTITY(1,1) PRIMARY KEY,
    goal_name NVARCHAR(100) UNIQUE
);

-- Table: Muscles Targeted
CREATE TABLE MusclesTargeted (
    muscle_id INT IDENTITY(1,1) PRIMARY KEY ,
    muscle_name NVARCHAR(100) UNIQUE
);

CREATE TABLE MemberMuscleTargets (
    target_id INT IDENTITY(1,1) PRIMARY KEY,
    member_id INT,
    muscle_id INT,
    target_day NVARCHAR(100), -- Assuming the target day can be represented as a string (e.g., Monday, Tuesday, etc.)
    FOREIGN KEY (member_id) REFERENCES gym_member(member_id),
    FOREIGN KEY (muscle_id) REFERENCES MusclesTargeted(muscle_id)
);


-- Table: Exercises
CREATE TABLE Exercises (
    exercise_id INT IDENTITY(1,1) PRIMARY KEY,
    exercise_name NVARCHAR(100) UNIQUE,
    muscle_target_id INT,
    FOREIGN KEY (muscle_target_id) REFERENCES MusclesTargeted(muscle_id)
);

-- Table: Workout Plans
CREATE TABLE WorkoutPlans (
    plan_id INT IDENTITY(1,1) PRIMARY KEY,
	trainer_id INT,
    member_id INT,
    goal_id INT,
    start_date DATE,
    end_date DATE,
	FOREIGN KEY (trainer_id) REFERENCES Trainer(trainer_id),
    FOREIGN KEY (member_id) REFERENCES gym_member(member_id),
    FOREIGN KEY (goal_id) REFERENCES FitnessGoals(goal_id)
);

CREATE TABLE PlanDetails (
    detail_id INT IDENTITY(1,1) PRIMARY KEY,
    plan_id INT,
    exercise_id INT,
    sets INT,
    reps INT,
    rest_intervals INT,
    FOREIGN KEY (plan_id) REFERENCES WorkoutPlans(plan_id),
    FOREIGN KEY (exercise_id) REFERENCES Exercises(exercise_id)
);

--drop table PlanDetails

CREATE TABLE GymOwners (
    onwer_id INT IDENTITY(1,1)  PRIMARY KEY,
	owner_name NVARCHAR(MAX),
    username NVARCHAR(MAX) NOT NULL,
    password NVARCHAR(MAX) NOT NULL,
    email NVARCHAR(MAX),
    contact_number NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Gym (
    gym_id INT IDENTITY(1,1) PRIMARY KEY,
    gym_ownerID INT,
    gym_name NVARCHAR(20),
    location NVARCHAR(30),
    phone NUMERIC(20),
    status NVARCHAR(20) DEFAULT 'Pending', --Pending, Approved, Denied
    FOREIGN KEY (gym_ownerID) REFERENCES GymOwners(onwer_id)
);


--drop table Gym

-- Table: Diet_Plans
CREATE TABLE Diet_Plans (
    PlanID INT IDENTITY(1,1) PRIMARY KEY,
    MemberID INT NOT NULL,
    TrainerID INT,
    Start_Date DATE,
    End_Date DATE,
	Allergens NVARCHAR(100),
    FOREIGN KEY (MemberID) REFERENCES gym_member(member_id),
    FOREIGN KEY (TrainerID) REFERENCES Trainer(trainer_id)
);

-- Table: Meals
CREATE TABLE Meals (
    MealID INT IDENTITY(1,1) PRIMARY KEY,
    PlanID INT NOT NULL,
    Meal_Name NVARCHAR(MAX),
    Description TEXT,
    FOREIGN KEY (PlanID) REFERENCES Diet_Plans(PlanID)
);

-- Table: Meal_Nutrition
CREATE TABLE Meal_Nutrition (
    NutritionID INT IDENTITY(1,1) PRIMARY KEY,
    MealID INT NOT NULL,
    Protein DECIMAL(10,2),
    Carbs DECIMAL(10,2),
    Fiber DECIMAL(10,2),
    Fat DECIMAL(10,2),
    FOREIGN KEY (MealID) REFERENCES Meals(MealID)
);

-- Table: Allergens
--CREATE TABLE Allergens (
--    AllergenID INT IDENTITY(1,1) PRIMARY KEY,
--    Allergen_Name NVARCHAR(MAX)
--);

--drop table Trainer_Gym_Association

CREATE TABLE Trainer_Gym_Association (
    trainer_id INT,
    gym_id INT,
	status VARCHAR(MAX) Default 'Pending'
    FOREIGN KEY (trainer_id) REFERENCES Trainer(trainer_id),
    FOREIGN KEY (gym_id) REFERENCES Gym(gym_id)
);


--create table Trainer_DietPlan_Association (
--	trainer_id INT,
--    dietPlan_id INT,
--    FOREIGN KEY (trainer_id) REFERENCES Trainer(trainer_id),
--    FOREIGN KEY (dietPlan_id) REFERENCES Diet_Plans(PlanID) 
--);

--create table Trainer_WorkoutPlan_Association (
--	trainer_id INT,
--    workoutPlan_id INT,
--    FOREIGN KEY (trainer_id) REFERENCES Trainer(trainer_id),
--    FOREIGN KEY (workoutPlan_id) REFERENCES WorkoutPlans(plan_id)
--);

CREATE TABLE ClientFeedback (
    feedback_id INT IDENTITY(1,1) PRIMARY KEY,
    trainer_id INT,
    member_id INT, 
    feedback_text NVARCHAR(MAX),
    rating INT,
    FOREIGN KEY (trainer_id) REFERENCES Trainer(trainer_id),
    FOREIGN KEY (member_id) REFERENCES gym_member(member_id)
);


CREATE TABLE Admin (
    admin_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(20),
    password NVARCHAR(20)
);




DROP TABLE ClientFeedback;
--DROP TABLE Trainer_DietPlan_Association;
--DROP TABLE Trainer_WorkoutPlan_Association;
DROP TABLE Trainer_Gym_Association;


DROP TABLE Meal_Nutrition;
DROP TABLE Meals;
DROP TABLE Diet_Plans;


DROP TABLE PlanDetails;
DROP TABLE WorkoutPlans;
DROP TABLE Exercises;
DROP TABLE MemberMuscleTargets;
DROP TABLE MusclesTargeted;
DROP TABLE FitnessGoals;
DROP TABLE Personal_Sessions;
DROP TABLE Trainer;
DROP TABLE Member_Gym_Association;
DROP TABLE Trainer_Gym_Association;
DROP TABLE Member_Trainer_Association;
DROP TABLE gym_member;
DROP TABLE GymOwners;
DROP TABLE Gym;
DROP TABLE Admin;


-- Inserting sample data into gym_member table
INSERT INTO gym_member (first_name, last_name, username, user_password, gender, dob, phone_number, email_address, user_address)
VALUES 
('John', 'Doe', 'johndoe', 'password123', 'Male', '1990-05-15', '1234567890', 'john.doe@example.com', '123 Main St'),
('Alice', 'Smith', 'alicesmith', 'alicepassword', 'Female', '1985-08-22', '9876543210', 'alice.smith@example.com', '456 Oak St'),
('Bob', 'Johnson', 'bobjohnson', 'bobpassword', 'Male', '1978-11-10', '5551234567', 'bob.johnson@example.com', '789 Elm St'),
('Emily', 'Brown', 'emilybrown', 'emilypassword', 'Female', '1993-03-28', '4447890123', 'emily.brown@example.com', '321 Pine St'),
('Michael', 'Davis', 'michaeldavis', 'michaelpassword', 'Male', '1980-09-17', '1112223333', 'michael.davis@example.com', '654 Birch St');

INSERT INTO gym_member (first_name, last_name, username, user_password, gender, dob, phone_number, email_address, user_address)
VALUES 
('Alice', 'Smith', 'alicesmith', 'alicepassword', 'Female', '1985-08-22', '9876543210', 'alice.smith@example.com', '456 Oak St');
INSERT INTO gym_member (first_name, last_name, username, user_password, gender, dob, phone_number, email_address, user_address)
VALUES 
('Taha', 'Mahmood', '1', '1', 'Male', '1990-05-15', '1234567890', 'tm@gmail.com', 'Lhr');

INSERT INTO gym_member (first_name, last_name, username, user_password, gender, dob, phone_number, email_address, user_address)
VALUES 
('Azeem', 'Chaudhry', '2', '2', 'Male', '1995-05-19', '1234567890', 'ma@gmail.com', 'Isl');


-- Inserting sample data into Trainer table
INSERT INTO Trainer (first_name, last_name, username, password, gender, DOB, phone_number, email, address, qualifications, specialty_areas, experience)
VALUES 
('Sarah', 'Jones', 'sarahjones', 'sarahpassword', 'Female', '1987-07-20', '9998887777', 'sarah.jones@example.com', '123 Park Ave', 'Certified Personal Trainer', 'Weight Loss, Strength Training', '5 years'),
('David', 'Lee', 'davidlee', 'davidpassword', 'Male', '1983-04-12', '3334445555', 'david.lee@example.com', '456 Maple Ave', 'Masters in Exercise Science', 'Athletic Conditioning, Endurance Training', '8 years'),
('Emma', 'Garcia', 'emmagarcia', 'emmapassword', 'Female', '1991-10-30', '7776665555', 'emma.garcia@example.com', '789 Oak Ave', 'Bachelors in Physical Education', 'Flexibility, Yoga', '3 years'),
('James', 'Wilson', 'jameswilson', 'jamespassword', 'Male', '1975-12-05', '2223334444', 'james.wilson@example.com', '987 Elm Ave', 'NASM Certified Personal Trainer', 'Bodybuilding, Nutrition', '10 years'),
('Sophia', 'Martinez', 'sophiamartinez', 'sophiapassword', 'Female', '1989-01-15', '6665554444', 'sophia.martinez@example.com', '654 Pine Ave', 'ISSA Certified Fitness Trainer', 'Pilates, CrossFit', '6 years');

INSERT INTO Trainer (first_name, last_name, username, password, gender, DOB, phone_number, email, address, qualifications, specialty_areas, experience)
VALUES 
('Hania', 'Fayyaz', '1', '1', 'Female', '2003-6-10', '1234567890', 'hf@gmail.com', 'Lhr', 'Certified Personal Trainer', 'Weight Loss, Strength Training', '5 years');

INSERT INTO Trainer (first_name, last_name, username, password, gender, DOB, phone_number, email, address, qualifications, specialty_areas, experience)
VALUES 
('Fatima', 'Hameed', '2', '2', 'Female', '2004-01-01', '1234567890', 'fh@gmail.com', 'Isl', 'Certified Personal Trainer', 'Weight Gain, Strength Training', '3 years');
-- Inserting sample data into FitnessGoals table
INSERT INTO FitnessGoals (goal_name)
VALUES 
('Weight Loss'),
('Muscle Gain'),
('Endurance'),
('Flexibility'),
('Overall Health');

-- Inserting sample data into MusclesTargeted table
INSERT INTO MusclesTargeted (muscle_name)
VALUES 
('Quadriceps'),
('Hamstrings'),
('Glutes'),
('Chest'),
('Back');

INSERT INTO MemberMuscleTargets (member_id, muscle_id, target_day) VALUES
(1, 1, 'Monday'),   -- Member 1 targets Biceps on Monday
(1, 2, 'Wednesday'), -- Member 1 targets Triceps on Wednesday
(2, 3, 'Tuesday'),   -- Member 2 targets Chest on Tuesday
(2, 4, 'Thursday'),  -- Member 2 targets Back on Thursday
(3, 5, 'Friday');    -- Member 3 targets Legs on Friday

-- Inserting sample data into Exercises table
INSERT INTO Exercises (exercise_name, muscle_target_id)
VALUES 
('Squats', 1),
('Deadlifts', 2),
('Hip Thrusts', 3),
('Bench Press', 4),
('Pull-Ups', 5);

-- Inserting sample data into WorkoutPlans table
INSERT INTO WorkoutPlans (trainer_id, member_id, goal_id, start_date, end_date)
VALUES 
(1, 1, 1, '2024-05-01', '2024-06-01'),
(2, 2, 2, '2024-05-01', '2024-06-01'),
(3, 3, 3, '2024-05-01', '2024-06-01'),
(4, 4, 4, '2024-05-01', '2024-06-01'),
(5, 5, 5, '2024-05-01', '2024-06-01');

-- Inserting sample data into PlanDetails table
INSERT INTO PlanDetails (plan_id, exercise_id, sets, reps, rest_intervals)
VALUES 
(1, 1, 3, 10, 60),
(2, 2, 3, 8, 90),
(3, 3, 3, 12, 45),
(4, 4, 3, 8, 90),
(5, 5, 3, 8, 90);

-- Inserting sample data into Gym table
INSERT INTO Gym (gym_ownerID, gym_name, location, phone)
VALUES 
(1, 'Fitness Paradise', '123 Main St', '1112223333'),
(2, 'Strength Zone', '456 Oak St', '2223334444'),
(3, 'Flex Gym', '789 Elm St', '3334445555'),
(4, 'Body Sculpt', '321 Pine St', '4445556666'),
(5, 'Health Hub', '654 Birch St', '5556667777');

-- Inserting sample data into Diet_Plans table
INSERT INTO Diet_Plans (MemberID, TrainerID, Plan_Name, Start_Date, End_Date)
VALUES 
(1, 1, 'Weight Loss Plan', '2024-05-01', '2024-06-01'),
(2, 2, 'Muscle Gain Plan', '2024-05-01', '2024-06-01'),
(3, 3, 'Endurance Plan', '2024-05-01', '2024-06-01'),
(4, 4, 'Flexibility Plan', '2024-05-01', '2024-06-01'),
(5, 5, 'Overall Health Plan', '2024-05-01', '2024-06-01');

-- Inserting sample data into Meals table
INSERT INTO Meals (PlanID, Meal_Name, Description, Time_of_Day, Allergens)
VALUES 
(1, 'Breakfast', 'Oatmeal with fruits', 'Morning', 'Nuts'),
(2, 'Lunch', 'Grilled chicken with vegetables', 'Afternoon', 'None'),
(3, 'Dinner', 'Salmon with quinoa', 'Evening', 'Seafood'),
(4, 'Snack', 'Greek yogurt with berries', 'Afternoon', 'Dairy'),
(5, 'Dinner', 'Vegetable stir-fry', 'Evening', 'None');

-- Inserting sample data into Meal_Nutrition table
INSERT INTO Meal_Nutrition (MealID, Protein, Carbs, Fiber, Fat)
VALUES 
(1, 20.5, 30.2, 5.3, 10.1),
(2, 25.3, 15.8, 3.9, 8.2),
(3, 30.1, 25.6, 6.7, 12.4),
(4, 15.9, 10.4, 2.1, 5.8),
(5, 18.7, 22.3, 4.5, 9.6);

-- Inserting sample data into Allergens table
INSERT INTO Allergens (Allergen_Name)
VALUES 
('Nuts'),
('Dairy'),
('Seafood'),
('Gluten'),
('Soy');

-- Inserting into Personal_Sessions table
INSERT INTO Personal_Sessions (MemberID, TrainerID, Date_and_Time, Status)
VALUES 
    (1, 1, '2024-05-01 10:00:00', 'Accepted'),
    (2, 2, '2024-05-02 11:00:00', 'Pending'),
    (3, 3, '2024-05-03 12:00:00', 'Accepted'),
    (4, 4, '2024-05-04 13:00:00', 'Rejected'),
    (5, 5, '2024-05-05 14:00:00', 'Pending'),
    (6, 6, '2024-05-06 15:00:00', 'Accepted'),
    (7, 7, '2024-05-07 16:00:00', 'Pending'),
    (8, 8, '2024-05-08 17:00:00', 'Accepted'),
    (9, 9, '2024-05-09 18:00:00', 'Pending'),
    (10, 10, '2024-05-10 19:00:00', 'Accepted');

-- Inserting sample data into Trainer_Gym_Association table
INSERT INTO Trainer_Gym_Association (trainer_id, gym_id)
VALUES 
(1, 6),
(2, 2),
(3, 3),
(4, 4),
(5, 5);


INSERT INTO Trainer_Gym_Association (trainer_id, gym_id)
VALUES 
(1, 1);

INSERT INTO Trainer_Gym_Association (trainer_id, gym_id)
VALUES 
(2, 1);


-- Inserting sample data into ClientFeedback table
INSERT INTO ClientFeedback (trainer_id, member_id, feedback_text, rating)
VALUES 
(1, 1, 'Sarah is excellent trainer, very motivating!', 5),
(2, 2, 'David helped me achieve my goals, highly recommend him.', 4),
(3, 3, 'Emma is very knowledgeable about endurance training.', 4),
(4, 4, 'James created a flexible plan that worked for me.', 5),
(5, 5, 'Sophia workouts are challenging but effective.', 4);

-- Inserting sample data into Admin table
INSERT INTO Admin (username, password)
VALUES 
('admin', 'admin')

-- Inserting sample data into GymOwners table
INSERT INTO GymOwners (owner_name, username, password, email, gym_name, location, contact_number, registration_date)
VALUES 
('Mark Johnson', 'markjohnson', 'markpassword', 'mark.johnson@example.com', 'Fitness Paradise', '123 Main St', '1112223333', '2024-01-15'),
('Emma Wilson', 'emmawilson', 'emmapassword', 'emma.wilson@example.com', 'Strength Zone', '456 Oak St', '2223334444', '2024-02-20'),
('Luke Smith', 'lukesmith', 'lukepassword', 'luke.smith@example.com', 'Flex Gym', '789 Elm St', '3334445555', '2024-03-10'),
('Sophia Brown', 'sophiabrown', 'sophiapassword', 'sophia.brown@example.com', 'Body Sculpt', '321 Pine St', '4445556666', '2024-04-05'),
('Michael Davis', 'michaeldavis_owner', 'michaelpassword_owner', 'michael.davis@example.com', 'Health Hub', '654 Birch St', '5556667777', '2024-05-01');


INSERT INTO GymOwners (owner_name, username, password, email, contact_number)
VALUES 
('Ali Khan', '1', '1', 'ak@gmail.com','1112223333');


-- Inserting sample data into Personal_Sessions table
INSERT INTO Personal_Sessions (MemberID, TrainerID, Date_and_Time, Status)
VALUES 
(1, 1, '2024-05-01 10:00:00', 'Pending'),
(2, 2, '2024-05-02 11:00:00', 'Pending'),
(3, 3, '2024-05-03 12:00:00', 'Pending'),
(4, 4, '2024-05-04 13:00:00', 'Pending'),
(5, 5, '2024-05-05 14:00:00', 'Pending');

-- Inserting sample data into Gym table
INSERT INTO Gym (gym_ownerID, gym_name, location, phone, status)
VALUES 
(1, 'Fitness Paradise', '123 Main St', '1112223333', 'Pending'),
(2, 'Strength Zone', '456 Oak St', '2223334444', 'Pending'),
(3, 'Flex Gym', '789 Elm St', '3334445555', 'Pending'),
(4, 'Body Sculpt', '321 Pine St', '4445556666', 'Pending'),
(5, 'Health Hub', '654 Birch St', '5556667777', 'Pending');

-- Inserting sample data into Trainer_Gym_Association table
--INSERT INTO Trainer_Gym_Association (trainer_id, gym_id)
--VALUES 
--(1, 1),
--(2, 2),
--(3, 3),
--(4, 4),
--(5, 5);

CREATE TABLE Member_Gym_Association (
    member_id INT,
    gym_id INT,
    FOREIGN KEY (member_id) REFERENCES gym_member(member_id),
    FOREIGN KEY (gym_id) REFERENCES Gym(gym_id)
);

CREATE TABLE Member_Trainer_Association (
    member_id INT,
    trainer_id INT,
    FOREIGN KEY (member_id) REFERENCES gym_member(member_id),
    FOREIGN KEY (trainer_id) REFERENCES Trainer(trainer_id)
);

INSERT INTO Member_Trainer_Association (member_id, trainer_id) VALUES (1,1);
INSERT INTO Member_Trainer_Association (member_id, trainer_id) VALUES (2,1);

INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (1, 6);
INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (2, 2);
INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (3, 3);
INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (4, 5);
INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (5, 4);

INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (1, 1);
INSERT INTO Member_Gym_Association (member_id, gym_id) VALUES (2, 1);


use [Flex Trainer];

-- Select all rows from gym_member table
SELECT * FROM gym_member;

-- Select all rows from Trainer table
SELECT * FROM Trainer;
		
-- Select all rows from Personal_Sessions table
SELECT * FROM Personal_Sessions;

-- Select all rows from FitnessGoals table
SELECT * FROM FitnessGoals;

-- Select all rows from MusclesTargeted table
SELECT * FROM MusclesTargeted;

SELECT * FROM MemberMuscleTargets;

-- Select all rows from Exercises table
SELECT * FROM Exercises;

-- Select all rows from WorkoutPlans table
SELECT * FROM WorkoutPlans;

-- Select all rows from PlanDetails table
SELECT * FROM PlanDetails;

-- Select all rows from Gym table
SELECT * FROM Gym;

-- Select all rows from Diet_Plans table
SELECT * FROM Diet_Plans;

-- Select all rows from Meals table
SELECT * FROM Meals;

-- Select all rows from Meal_Nutrition table
SELECT * FROM Meal_Nutrition;

-- Select all rows from Allergens table
--SELECT * FROM Allergens;

-- Select all rows from Trainer_Gym_Association table
SELECT * FROM Trainer_Gym_Association;
SELECT * FROM Member_Trainer_Association;


-- Select all rows from ClientFeedback table
SELECT * FROM ClientFeedback;

-- Select all rows from Admin table
SELECT * FROM Admin;

-- Select all rows from GymOwners table
SELECT * FROM GymOwners;

SELECT * FROM Member_Gym_Association;

SELECT * FROM FitnessGoals;	



-- Reportsssss



--1
SELECT gm.first_name, gm.last_name, gm.gender,gm.dob,gm.phone_number
FROM gym_member gm
JOIN Member_Gym_Association mga ON gm.member_id = mga.member_id
JOIN Member_Trainer_Association mta ON gm.member_id = mta.member_id
JOIN Trainer t ON mta.trainer_id = t.trainer_id
JOIN Gym g ON mga.gym_id = g.gym_id
WHERE g.gym_name = 'Fitness Hub'
AND t.first_name = 'Hania'
AND t.last_name = 'Fayyaz';


--2
SELECT gm.*
FROM gym_member gm
JOIN Member_Gym_Association mga ON gm.member_id = mga.member_id
JOIN Diet_Plans dp ON gm.member_id = dp.MemberID
WHERE mga.gym_id = (SELECT gym_id FROM Gym WHERE gym_name = 'Fitness Hub')
AND dp.PlanID = (SELECT PlanID FROM Diet_Plans WHERE MemberID = gm.member_id AND PlanID = 1);


--3
SELECT gm.*
FROM gym_member gm
JOIN Member_Trainer_Association mta ON gm.member_id = mta.member_id
JOIN Diet_Plans dp ON gm.member_id = dp.MemberID
WHERE mta.trainer_id = 1
AND dp.PlanID = 1;


--4
SELECT COUNT(DISTINCT mmt.member_id) AS member_count
FROM MemberMuscleTargets mmt
JOIN Exercises e ON mmt.muscle_id = e.muscle_target_id
JOIN PlanDetails pd ON e.exercise_id = pd.exercise_id
JOIN WorkoutPlans wp ON pd.plan_id = wp.plan_id
JOIN gym_member gm ON wp.member_id = gm.member_id
JOIN Member_Gym_Association mga ON gm.member_id = mga.member_id
JOIN Gym g ON mga.gym_id = g.gym_id
WHERE g.gym_name = 'Fitness Hub'
AND e.exercise_name IN ('Leg Press');


--5


SELECT DISTINCT dp.*
FROM Diet_Plans dp
JOIN Meals m ON dp.PlanID = m.PlanID
JOIN Meal_Nutrition mn ON m.MealID = mn.MealID
WHERE m.Meal_Name = 'Breakfast'
AND (mn.Protein + mn.Carbs + mn.Fiber + mn.Fat) < 500;


--6
SELECT DISTINCT dp.*
FROM Diet_Plans dp
JOIN Meals m ON dp.PlanID = m.PlanID
JOIN Meal_Nutrition mn ON m.MealID = mn.MealID
WHERE m.Meal_Name = 'Breakfast'
AND mn.Carbs < 300;


--7 
SELECT DISTINCT wp.*
FROM WorkoutPlans wp
JOIN PlanDetails pd ON wp.plan_id = pd.plan_id
JOIN Exercises e ON pd.exercise_id = e.exercise_id
WHERE e.exercise_name IN ('Push Ups', 'Pull Ups', 'Squats');

--8

SELECT *
FROM Diet_Plans
WHERE Allergens IS NULL OR Allergens NOT LIKE '%peanuts%';

--9



--10
SELECT g.gym_name, COUNT(DISTINCT gm.member_id) AS total_members
FROM Gym g
JOIN Member_Gym_Association mga ON g.gym_id = mga.gym_id
JOIN gym_member gm ON mga.member_id = gm.member_id
GROUP BY g.gym_name;

--11 (total number of trainers)
SELECT COUNT(*) AS total_trainers FROM Trainer;


--12 (Total number of gyms with approved status)
SELECT COUNT(*) AS total_approved_gyms FROM Gym WHERE status = 'Approved';

--13 (Total number of unique muscle groups targeted by exercises)
SELECT COUNT(DISTINCT muscle_id) AS total_unique_muscle_groups FROM MusclesTargeted;


--14 (Total number of pending personal training sessions)
SELECT COUNT(*) AS total_pending_sessions FROM Personal_Sessions WHERE Status = 'Pending';


--15 Total number of diet plans with allergen information;
SELECT COUNT(*) AS total_diet_plans_with_allergens 
FROM Diet_Plans 
WHERE Allergens IS NOT NULL AND Allergens <> '';


--16 Total number of distinct gym owners
SELECT COUNT(DISTINCT owner_name) AS total_gym_owners FROM GymOwners;

--17 Total number of exercises targeting specific muscle groups

SELECT COUNT(*) AS total_exercises FROM Exercises;


--18 Total number of unique member addresses
SELECT COUNT(DISTINCT user_address) AS total_unique_addresses FROM gym_member;


--19 Total number of distinct specialties among trainers
SELECT COUNT(DISTINCT specialty_areas) AS total_specialties FROM Trainer;


--20 Total number of unique gym locations
SELECT COUNT(DISTINCT location) AS total_unique_locations FROM Gym;



-- Inserting into gym_member table
INSERT INTO gym_member (first_name, last_name, username, user_password, gender, dob, phone_number, email_address, user_address)
VALUES 
    ('John', 'Doe', 'john_doe', 'password123', 'Male', '1990-05-20', '1234567890', 'john@example.com', '123 Street, City'),
    ('Jane', 'Smith', 'jane_smith', 'password456', 'Female', '1985-08-15', '9876543210', 'jane@example.com', '456 Avenue, Town'),
    ('Michael', 'Johnson', 'michael_j', 'pass789', 'Male', '1982-03-10', '5551234567', 'michael@example.com', '789 Road, Village'),
    ('Emily', 'Davis', 'em_davis', 'abc123', 'Female', '1995-11-28', '9998887776', 'emily@example.com', '321 Lane, Suburb'),
    ('David', 'Brown', 'david_b', 'def456', 'Male', '1988-07-03', '1112223333', 'david@example.com', '654 Drive, Countryside'),
    ('Alex', 'Johnson', 'alex_j', 'alexpass', 'Male', '1993-02-14', '3334445555', 'alex@example.com', '987 Hill, City'),
    ('Samantha', 'White', 'samantha_w', 'samanthapass', 'Female', '1992-09-30', '7778889999', 'samantha@example.com', '654 Mountain, Town'),
    ('Daniel', 'Clark', 'daniel_c', 'danielpass', 'Male', '1987-04-22', '8889990000', 'daniel@example.com', '123 Ridge, City'),
    ('Sophia', 'Martinez', 'sophia_m', 'sophiapass', 'Female', '1998-12-05', '2223334444', 'sophia@example.com', '456 Valley, Suburb'),
    ('Ethan', 'Taylor', 'ethan_t', 'ethanpass', 'Male', '1991-10-17', '4445556666', 'ethan@example.com', '789 Lane, Countryside');

-- Inserting into Trainer table
INSERT INTO Trainer (first_name, last_name, username, password, gender, dob, phone_number, email, address, qualifications, specialty_areas, experience)
VALUES 
    ('Sarah', 'Johnson', 'sarah_j', 'trainerpass', 'Female', '1980-12-10', '4445556666', 'sarah@example.com', '987 Hill, City', 'Certified Personal Trainer', 'Weight Loss, Strength Training', '10 years'),
    ('Robert', 'Lee', 'robert_lee', 'trainerpass123', 'Male', '1975-09-15', '7778889999', 'robert@example.com', '654 Mountain, Town', 'Fitness Instructor Certification', 'Cardio, Flexibility', '15 years'),
    ('Emma', 'Anderson', 'emma_a', 'emma123', 'Female', '1983-06-25', '5556667777', 'emma@example.com', '321 Peak, Suburb', 'Fitness Coach Certification', 'Yoga, Pilates', '12 years'),
    ('William', 'Garcia', 'william_g', 'williampass', 'Male', '1986-01-12', '6667778888', 'william@example.com', '789 Valley, City', 'Personal Trainer Certification', 'CrossFit, HIIT', '8 years'),
    ('Olivia', 'Lopez', 'olivia_l', 'oliviapass', 'Female', '1990-08-05', '9990001111', 'olivia@example.com', '456 Summit, Town', 'Strength and Conditioning Certification', 'Functional Training, Sports Specific', '10 years'),
    ('James', 'Perez', 'james_p', 'jamespass', 'Male', '1978-03-20', '1112223333', 'james@example.com', '123 Peak, Countryside', 'Nutrition Certification', 'Bodybuilding, Powerlifting', '15 years'),
    ('Ava', 'Rodriguez', 'ava_r', 'avapass', 'Female', '1989-11-15', '2223334444', 'ava@example.com', '654 Ridge, City', 'Group Fitness Instructor Certification', 'Zumba, Dance Fitness', '10 years'),
    ('Noah', 'Hernandez', 'noah_h', 'noahpass', 'Male', '1981-05-30', '3334445555', 'noah@example.com', '987 Summit, Suburb', 'Circuit Training Certification', 'Bootcamp, TRX', '12 years'),
    ('Isabella', 'Gonzalez', 'isabella_g', 'isabellapass', 'Female', '1984-09-18', '8889990000', 'isabella@example.com', '123 Peak, Town', 'Athletic Training Certification', 'Agility, Speed Training', '8 years'),
    ('Liam', 'Rivera', 'liam_r', 'liampass', 'Male', '1982-07-08', '9991112222', 'liam@example.com', '456 Valley, Countryside', 'Rehabilitation Certification', 'Injury Prevention, Corrective Exercise', '10 years');

-- Inserting into MusclesTargeted table
INSERT INTO MusclesTargeted (muscle_name)
VALUES 
    ('Chest'),
    ('Back'),
    ('Legs'),
    ('Arms'),
    ('Shoulders'),
    ('Abs'),
    ('Glutes'),
    ('Hamstrings'),
    ('Calves'),
    ('Triceps');

-- Inserting into FitnessGoals table
INSERT INTO FitnessGoals (goal_name)
VALUES 
    ('Weight Loss'),
    ('Muscle Gain'),
    ('Improved Endurance'),
    ('Core Strength'),
    ('Flexibility'),
    ('Injury Rehabilitation'),
    ('Athletic Performance Improvement'),
    ('Body Toning'),
    ('Cardiovascular Health Improvement'),
    ('Stress Reduction');

	-- Inserting into MemberMuscleTargets table
INSERT INTO MemberMuscleTargets (member_id, muscle_id, target_day)
VALUES 
    (1, 1, 'Monday'),
    (1, 2, 'Tuesday'),
    (2, 3, 'Wednesday'),
    (2, 4, 'Thursday'),
    (3, 5, 'Friday'),
    (3, 1, 'Saturday'),
    (4, 2, 'Sunday'),
    (4, 3, 'Monday'),
    (5, 4, 'Tuesday'),
    (5, 5, 'Wednesday');

-- Inserting into Exercises table
INSERT INTO Exercises (exercise_name, muscle_target_id)
VALUES 
    ('Bench Press', 1),
    ('Deadlift', 2),
    ('Squat', 3),
    ('Bicep Curls', 4),
    ('Shoulder Press', 5),
    ('Plank', 6),
    ('Squats', 3),
    ('Lunges', 3),
    ('Tricep Dips', 10),
    ('Russian Twists', 6);

-- Inserting into WorkoutPlans table
INSERT INTO WorkoutPlans (trainer_id, member_id, goal_id, start_date, end_date)
VALUES 
    (1, 1, 1, '2024-05-01', '2024-05-30'),
    (2, 3, 2, '2024-05-05', '2024-06-05'),
    (3, 5, 3, '2024-05-10', '2024-06-10'),
    (4, 7, 4, '2024-05-15', '2024-06-15'),
    (5, 9, 5, '2024-05-20', '2024-06-20'),
    (6, 2, 6, '2024-05-25', '2024-06-25'),
    (7, 4, 7, '2024-05-30', '2024-06-30'),
    (8, 6, 8, '2024-06-01', '2024-07-01'),
    (9, 8, 9, '2024-06-05', '2024-07-05'),
    (10, 10, 10, '2024-06-10', '2024-07-10');

-- Inserting into PlanDetails table
INSERT INTO PlanDetails (plan_id, exercise_id, sets, reps, rest_intervals)
VALUES 
    (1, 1, 3, 12, 90),
    (1, 2, 4, 10, 120),
    (1, 3, 4, 10, 120),
    (2, 4, 3, 15, 90),
    (2, 5, 3, 12, 90),
    (3, 6, 4, 60, 60),
    (3, 7, 4, 15, 90),
    (3, 8, 3, 12, 90),
    (4, 9, 3, 15, 90),
    (4, 10, 3, 20, 90),
    (5, 1, 3, 10, 90),
    (5, 2, 4, 8, 120),
    (6, 3, 5, 10, 120),
    (6, 4, 3, 12, 90),
    (7, 5, 3, 10, 90),
    (7, 6, 4, 60, 60),
    (8, 7, 3, 15, 90),
    (8, 8, 3, 12, 90),
    (9, 9, 3, 15, 90),
    (9, 10, 3, 20, 90),
    (10, 1, 4, 12, 90),
    (10, 2, 5, 10, 120),
    (10, 3, 5, 10, 120);

-- Inserting into Trainer_Gym_Association table
INSERT INTO Trainer_Gym_Association (trainer_id, gym_id)
VALUES 
    (1, 1),
    (2, 2),
    (3, 1),
    (4, 2),
    (5, 1),
    (6, 2),
    (7, 1),
    (8, 2),
    (9, 1),
    (10, 2);

-- Inserting into ClientFeedback table
INSERT INTO ClientFeedback (trainer_id, member_id, feedback_text, rating)
VALUES 
    (1, 1, 'Great trainer, very motivating!', 5),
    (2, 3, 'Excellent workouts, saw results quickly.', 4),
    (3, 5, 'Amazing yoga sessions, helped improve flexibility.', 5),
    (4, 7, 'Great coaching for strength training, highly recommend!', 5),
    (5, 9, 'Fantastic workouts, saw improvements in endurance.', 4),
    (6, 2, 'The rehabilitation exercises were very helpful.', 5),
    (7, 4, 'Fun and challenging Zumba classes, loved it!', 4),
    (8, 6, 'Effective workouts for body toning, very satisfied.', 5),
    (9, 8, 'Great guidance for improving cardiovascular health.', 4),
    (10, 10, 'Excellent stress reduction techniques, feel much better.', 5);

-- Inserting into GymOwners table
INSERT INTO GymOwners (owner_name, username, password, email, contact_number)
VALUES 
    ('Gym Owner 1', 'gym_owner1', 'gymownerpass', 'owner1@example.com', '1234567890'),
    ('Gym Owner 2', 'gym_owner2', 'gymownerpass123', 'owner2@example.com', '9876543210'),
    ('Gym Owner 3', 'gym_owner3', 'gymownerpass456', 'owner3@example.com', '1112223333'),
    ('Gym Owner 4', 'gym_owner4', 'gymownerpass789', 'owner4@example.com', '4445556666'),
    ('Gym Owner 5', 'gym_owner5', 'gymownerpass101112', 'owner5@example.com', '7778889999'),
    ('Gym Owner 6', 'gym_owner6', 'gymownerpass131415', 'owner6@example.com', '9990001111'),
    ('Gym Owner 7', 'gym_owner7', 'gymownerpass161718', 'owner7@example.com', '2223334444'),
    ('Gym Owner 8', 'gym_owner8', 'gymownerpass192021', 'owner8@example.com', '5556667777'),
    ('Gym Owner 9', 'gym_owner9', 'gymownerpass222324', 'owner9@example.com', '8889990000'),
    ('Gym Owner 10', 'gym_owner10', 'gymownerpass252627', 'owner10@example.com', '1112223333');

-- Inserting into Gym table
INSERT INTO Gym (gym_ownerID, gym_name, location, phone)
VALUES 
    (1, 'Fitness Zone', '123 Main Street', '5551234567'),
    (2, 'Health Hub', '456 Elm Street', '9998887776'),
    (3, 'Strength Studio', '789 Oak Avenue', '1112223333'),
    (4, 'Flex Fitness', '321 Maple Lane', '4445556666'),
    (5, 'Cardio Craze', '654 Pine Street', '7778889999'),
    (6, 'Yoga Sanctuary', '987 Cedar Avenue', '9990001111'),
    (7, 'Zumba Zone', '654 Elmwood Drive', '2223334444'),
    (8, 'Tone Up Gym', '123 Cedar Lane', '5556667777'),
    (9, 'Heart Health Center', '456 Oakwood Avenue', '8889990000'),
    (10, 'Stress Relief Gym', '789 Maple Drive', '1112223333');

	UPDATE Gym
SET status = 'Approved';

	-- Inserting into Diet_Plans table
INSERT INTO Diet_Plans (MemberID, TrainerID, Start_Date, End_Date, Allergens)
VALUES 
    (1, 1, '2024-05-01', '2024-05-30', 'Nuts, Dairy'),
    (2, 2, '2024-05-05', '2024-06-05', 'Gluten'),
    (3, 3, '2024-05-10', '2024-06-10', 'Shellfish'),
    (4, 4, '2024-05-15', '2024-06-15', 'None'),
    (5, 5, '2024-05-20', '2024-06-20', 'Lactose Intolerant');

-- Inserting into Meals table
INSERT INTO Meals (PlanID, Meal_Name, Description)
VALUES 
    (1, 'Breakfast', 'Oatmeal with fruits'),
    (1, 'Lunch', 'Grilled chicken with vegetables'),
    (1, 'Dinner', 'Salmon with quinoa'),
    (2, 'Breakfast', 'Smoothie with spinach and berries'),
    (2, 'Lunch', 'Quinoa salad with chickpeas'),
    (2, 'Dinner', 'Stir-fried tofu with vegetables'),
    (3, 'Breakfast', 'Greek yogurt with granola'),
    (3, 'Lunch', 'Shrimp stir-fry with brown rice'),
    (3, 'Dinner', 'Grilled steak with sweet potato'),
    (4, 'Breakfast', 'Egg scramble with avocado'),
    (4, 'Lunch', 'Turkey sandwich with whole wheat bread'),
    (4, 'Dinner', 'Baked cod with asparagus'),
    (5, 'Breakfast', 'Almond milk smoothie with banana'),
    (5, 'Lunch', 'Quinoa bowl with mixed vegetables'),
    (5, 'Dinner', 'Tofu stir-fry with broccoli');

-- Inserting into Meal_Nutrition table
INSERT INTO Meal_Nutrition (MealID, Protein, Carbs, Fiber, Fat)
VALUES 
    (1, 15.5, 30.2, 5.3, 8.7),
    (2, 25.3, 20.1, 8.9, 12.4),
    (3, 22.1, 35.7, 6.6, 15.9),
    (4, 10.2, 40.5, 7.8, 5.3),
    (5, 18.7, 25.8, 9.1, 11.2),
    (6, 12.6, 28.3, 6.8, 9.8),
    (7, 20.5, 32.6, 7.2, 13.5),
    (8, 30.1, 18.9, 8.4, 10.7),
    (9, 16.9, 36.4, 6.1, 14.2),
    (10, 14.3, 27.5, 5.7, 8.9);

-- Inserting into Member_Gym_Association table
INSERT INTO Member_Gym_Association (member_id, gym_id)
VALUES 
    (1, 1),
    (2, 2),
    (3, 1),
    (4, 2),
    (5, 1);

-- Inserting into Member_Trainer_Association table
INSERT INTO Member_Trainer_Association (member_id, trainer_id)
VALUES 
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5);



-- Trigger for insertion
CREATE TRIGGER trg_insert_gym_member
ON gym_member
AFTER INSERT
AS
BEGIN
    PRINT 'New member inserted.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_gym_member
ON gym_member
AFTER DELETE
AS
BEGIN
    PRINT 'Member deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_gym_member
ON gym_member
AFTER UPDATE
AS
BEGIN
    PRINT 'Member information updated.';
END;
GO

-- Trigger for insertion
CREATE TRIGGER trg_insert_trainer
ON Trainer
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., logging, validation)
    PRINT 'New trainer inserted.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_trainer
ON Trainer
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Trainer deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_trainer
ON Trainer
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., logging, validation)
    PRINT 'Trainer information updated.';
END;
GO


-- Trigger for insertion
CREATE TRIGGER trg_insert_personal_sessions
ON Personal_Sessions
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'New personal session scheduled.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_personal_sessions
ON Personal_Sessions
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Personal session canceled or completed.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_personal_sessions
ON Personal_Sessions
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Personal session updated.';
END;
GO



-- Trigger for insertion
CREATE TRIGGER trg_insert_exercises
ON Exercises
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., logging, validation)
    PRINT 'New exercise added.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_exercises
ON Exercises
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Exercise deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_exercises
ON Exercises
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., logging, validation)
    PRINT 'Exercise information updated.';
END;
GO



-- Trigger for insertion
CREATE TRIGGER trg_insert_workout_plans
ON WorkoutPlans
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'New workout plan created.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_workout_plans
ON WorkoutPlans
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Workout plan deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_workout_plans
ON WorkoutPlans
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Workout plan updated.';
END;
GO



-- Trigger for insertion
CREATE TRIGGER trg_insert_plan_details
ON PlanDetails
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., validation, updating related data)
    PRINT 'New plan detail inserted.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_plan_details
ON PlanDetails
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Plan detail deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_plan_details
ON PlanDetails
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., validation, updating related data)
    PRINT 'Plan detail updated.';
END;
GO



-- Trigger for insertion
CREATE TRIGGER trg_insert_gym_owners
ON GymOwners
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'New gym owner added.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_gym_owners
ON GymOwners
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Gym owner deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_gym_owners
ON GymOwners
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Gym owner information updated.';
END;
GO



-- Trigger for insertion
CREATE TRIGGER trg_insert_gym
ON Gym
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'New gym added.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_gym
ON Gym
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Gym deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_gym
ON Gym
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Gym information updated.';
END;
GO



-- Trigger for insertion
CREATE TRIGGER trg_insert_diet_plans
ON Diet_Plans
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'New diet plan created.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_diet_plans
ON Diet_Plans
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Diet plan deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_diet_plans
ON Diet_Plans
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Diet plan updated.';
END;
GO




-- Trigger for insertion
CREATE TRIGGER trg_insert_meals
ON Meals
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'New meal added.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_meals
ON Meals
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Meal deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_meals
ON Meals
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Meal information updated.';
END;
GO


-- Trigger for insertion
CREATE TRIGGER trg_insert_meal_nutrition
ON Meal_Nutrition
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., validation, calculation)
    PRINT 'Nutritional information for a meal added.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_meal_nutrition
ON Meal_Nutrition
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Nutritional information for a meal deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_meal_nutrition
ON Meal_Nutrition
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., validation, calculation)
    PRINT 'Nutritional information for a meal updated.';
END;
GO


-- Trigger for insertion
CREATE TRIGGER trg_insert_trainer_gym_association
ON Trainer_Gym_Association
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Trainer assigned to a gym.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_trainer_gym_association
ON Trainer_Gym_Association
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Trainer removed from gym association.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_trainer_gym_association
ON Trainer_Gym_Association
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, validation)
    PRINT 'Trainer gym association updated.';
END;
GO



-- Trigger for insertion
CREATE TRIGGER trg_insert_client_feedback
ON ClientFeedback
AFTER INSERT
AS
BEGIN
    -- Your logic here (e.g., notification, analysis)
    PRINT 'New client feedback received.';
END;
GO

-- Trigger for deletion
CREATE TRIGGER trg_delete_client_feedback
ON ClientFeedback
AFTER DELETE
AS
BEGIN
    -- Your logic here (e.g., logging, referential integrity)
    PRINT 'Client feedback deleted.';
END;
GO

-- Trigger for update
CREATE TRIGGER trg_update_client_feedback
ON ClientFeedback
AFTER UPDATE
AS
BEGIN
    -- Your logic here (e.g., notification, analysis)
    PRINT 'Client feedback updated.';
END;
GO













