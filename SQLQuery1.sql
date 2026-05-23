CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200),
    BirthDate DATE,
    DoctorType INT NOT NULL,
    Salary DECIMAL(10,2),
    StartTraining DATE NULL,
    EndTraining DATE NULL
);
