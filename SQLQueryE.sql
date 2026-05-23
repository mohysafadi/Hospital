DROP TABLE IF EXISTS ExternalTreatment;
GO

CREATE TABLE ExternalTreatment
(
    ExternalTreatmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    TreatmentName NVARCHAR(200) NOT NULL,
    Cost DECIMAL(10,2) NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    VisitDate DATE NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);