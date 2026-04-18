CREATE TABLE mechanics (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone VARCHAR(20) UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE services (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price DECIMAL(10,2) NOT NULL CHECK (price > 0),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TYPE service_status AS ENUM ('pending', 'proses', 'selesai');

CREATE TABLE transactions (
    id SERIAL PRIMARY KEY,
    customer_name VARCHAR(100) NOT NULL,
    vehicle VARCHAR(100) NOT NULL,
    service_id INT NOT NULL,
    mechanic_id INT NOT NULL,
    service_date DATE NOT NULL,
    status service_status DEFAULT 'pending',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP NULL,
    FOREIGN KEY (service_id) REFERENCES services(id),
    FOREIGN KEY (mechanic_id) REFERENCES mechanics(id)
);

CREATE INDEX idx_service_id ON transactions(service_id);
CREATE INDEX idx_mechanic_id ON transactions(mechanic_id);

INSERT INTO mechanics (name, phone) VALUES
('Budi', '0822377778'),
('Andi', '0827893652'),
('Joko', '0856728763'),
('Rudi', '0812635718'),
('Agus', '0859936363');

INSERT INTO services (name, price) VALUES
('Ganti Oli', 50000),
('Servis Mesin', 150000),
('Ganti Kampas Rem', 80000),
('Tune Up', 120000),
('Cuci Motor', 30000);

INSERT INTO transactions (customer_name, vehicle, service_id, mechanic_id, service_date, status) VALUES
('Doni', 'Vario 125', 1, 1, '2024-06-01', 'selesai'),
('Siti', 'Beat', 2, 2, '2024-06-02', 'proses'),
('Rina', 'Nmax', 3, 3, '2024-06-03', 'pending'),
('Tono', 'Scoopy', 4, 4, '2024-06-04', 'selesai'),
('Dewi', 'Aerox', 5, 5, '2024-06-05', 'pending');

DROP TABLE IF EXISTS transactions;
DROP TABLE IF EXISTS services;
DROP TABLE IF EXISTS mechanics;

SELECT * FROM mechanics
SELECT * FROM services
SELECT * FROM transactions

TRUNCATE TABLE transactions RESTART IDENTITY;