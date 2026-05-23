CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200),
    BirthDate DATE,
    PatientType INT NOT NULL,   -- 1 Internal, 2 External
    IsDischarged BIT DEFAULT 0
);