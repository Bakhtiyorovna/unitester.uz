CREATE TABLE users(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	first_name VARCHAR(50),
	last_name VARCHAR(50),
	user_name VARCHAR(50),
	email VARCHAR(40),
	phone_number VARCHAR(13),
	rol TEXT,
	description TEXT,
	created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
	updated_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW()
)
drop table direction
CREATE TABLE directions(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	name VARCHAR(30),
	type TEXT,
	description TEXT,
	created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
	updated_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW()
)
DROP TABLE contests
CREATE TABLE contests(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	started_at TIMESTAMP WITHOUT TIME ZONE NOT NULL ,
	end_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,
	status TEXT,
	student_number INTEGER,
	description TEXT,
	created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
	updated_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW()
)

CREATE TABLE teacher_direction(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	teacher_id BIGINT REFERENCES users(id),
	direction_id BIGINT REFERENCES directions(id),
	test_number INTEGER,
	descriptioMP WITHOUT TIME ZONE DEFAULT NOW(),
	updated_at TIMESTAMP WITHOUT TIME Zn TEXT,
	created_at TIMESTAONE DEFAULT NOW()
)
DROP TABLE contets_student
CREATE TABLE contest_student(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	contest_id BIGINT REFERENCES contests (id),
	students_id BIGINT REFERENCES users (id),
	basic_direction_id BIGINT REFERENCES directions (id),
	second_direction_id BIGINT REFERENCES directions (id),
	total_result INTEGER,
	description TEXT
)
CREATE TABLE tests(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	test TEXT,
	direction_id BIGINT REFERENCES teacher_direction(id),
	type TEXT,
	variant_a TEXT,
	variant_b TEXT,
	variant_c TEXT,
	variant_d TEXT,
	right_variant TEXT,
	created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
	updated_at TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW()
)

CREATE TABLE student_test(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	test_id BIGINT REFERENCES tests(id),
	contest_students BIGINT REFERENCES contest_student(id),
	result BOOL
	)