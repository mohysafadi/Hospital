
CREATE TABLE InternalTreatment (
    InternalTreatmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    RoomNumber NVARCHAR(50) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    TreatmentName NVARCHAR(200) NOT NULL,
    Cost DECIMAL(10,2) NOT NULL,
    Notes NVARCHAR(MAX) NULL
);