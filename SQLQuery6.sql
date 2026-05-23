CREATE TABLE ExternalTreatment (
    TreatmentID INT PRIMARY KEY,
    ClinicNumber INT NOT NULL,
    DoctorID INT NOT NULL,

    FOREIGN KEY (TreatmentID) REFERENCES Treatments(TreatmentID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);