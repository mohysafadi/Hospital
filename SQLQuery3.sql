CREATE TABLE Treatments (
    TreatmentID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT NOT NULL,
    TreatmentDate DATE NOT NULL,
    Cost DECIMAL(10,2) NOT NULL,
    TreatmentType INT NOT NULL,   -- 1 Internal, 2 External

    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);