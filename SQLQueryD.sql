CREATE TABLE InternalTreatmentDoctor (
    InternalTreatmentID INT NOT NULL,
    DoctorID INT NOT NULL,
    PRIMARY KEY (InternalTreatmentID, DoctorID),
    FOREIGN KEY (InternalTreatmentID) REFERENCES InternalTreatment(InternalTreatmentID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);