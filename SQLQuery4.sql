CREATE TABLE InternalTreatment (
    TreatmentID INT PRIMARY KEY,
    GraduationDate DATE,
    DepartmentID INT,

    FOREIGN KEY (TreatmentID) REFERENCES Treatments(TreatmentID)
);