CREATE TABLE InternalTreatmentDoctors (
    TreatmentID INT NOT NULL,
    DoctorID INT NOT NULL,

    FOREIGN KEY (TreatmentID) REFERENCES InternalTreatment(TreatmentID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);