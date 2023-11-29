CREATE TABLE Clients
(
    Id SERIAL PRIMARY KEY,
    Name TEXT,
    Number TEXT,
    Password TEXT,
    UrgentAccount TEXT,
    DemandAccount TEXT,
    CardAccount TEXT
);

    CREATE UNIQUE INDEX unique_number ON Clients(Number);
    CREATE INDEX idx_number_password_idx ON Clients(Number, Password);