CREATE DATABASE mediccare;

CREATE TABLE IF NOT EXISTS patient (
    id SERIAL PRIMARY KEY,
    firstname VARCHAR(50),
    lastname VARCHAR(50),
	age int
);

CREATE TABLE IF NOT EXISTS facility (
	id SERIAL PRIMARY KEY,
    facility_name VARCHAR(50),
    city VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS payer (
	id SERIAL PRIMARY KEY,
    payer_name VARCHAR(50),
    city VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS encounter (
	id SERIAL PRIMARY KEY,
	patient_id INTEGER NOT NULL,
	facility_id INTEGER NOT NULL,
	payer_id INTEGER NOT NULL,
    FOREIGN KEY (patient_id) REFERENCES patient (id),
	FOREIGN KEY (facility_id) REFERENCES facility (id),
	FOREIGN KEY (payer_id) REFERENCES payer (id)
);

INSERT INTO patient (id, firstname, lastname, age) VALUES (1, 'name1', 'lastname1', 20);
INSERT INTO patient (id, firstname, lastname, age) VALUES (2, 'name2', 'lastname2', 22);
INSERT INTO patient (id, firstname, lastname, age) VALUES (3, 'name3', 'lastname3', 23);
INSERT INTO patient (id, firstname, lastname, age) VALUES (4, 'name4', 'lastname4', 14);
INSERT INTO patient (id, firstname, lastname, age) VALUES (5, 'name5', 'lastname5', 15);

INSERT INTO facility (id, facility_name, city) VALUES (1, 'facility_name1', 'city1');
INSERT INTO facility (id, facility_name, city) VALUES (2, 'facility_name2', 'city1');
INSERT INTO facility (id, facility_name, city) VALUES (3, 'facility_name3', 'city2');
INSERT INTO facility (id, facility_name, city) VALUES (4, 'facility_name4', 'city2');

INSERT INTO payer (id, payer_name, city) VALUES (1, 'payer_name1', 'city1');
INSERT INTO payer (id, payer_name, city) VALUES (2, 'payer_name2', 'city1');
INSERT INTO payer (id, payer_name, city) VALUES (3, 'payer_name3', 'city2');
INSERT INTO payer (id, payer_name, city) VALUES (4, 'payer_name4', 'city2');

-- Encounters patient 1
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (1, 1, 1, 1);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (2, 1, 1, 1);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (3, 1, 1, 1);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (4, 1, 1, 1);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (5, 1, 2, 2);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (6, 1, 2, 2);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (7, 1, 3, 3);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (8, 1, 3, 3);

--Won't appear because of same insurance city
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (9, 2, 3, 3);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (10, 2, 3, 3);

--Will appear with two cities and should appear before Encounters for patient_id = 1
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (11, 3, 3, 1);
INSERT INTO encounter (id, patient_id, facility_id, payer_id) VALUES (12, 3, 3, 3);









