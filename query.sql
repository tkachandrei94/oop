# Создание таблицы
CREATE TABLE students (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100),
    age INT
);

# Добавление новой колонки
ALTER TABLE students ADD email VARCHAR(100);

# Вставка данных
INSERT INTO students (name, age, email)
VALUES
('Andrii Tkach', 18, 'andrei@example.com'),
('Tarasov Taras', 19, 'tarasov@example.com'),
('Petro Petrov', 25, 'petrov@example.com');

# Запрос всех записей
SELECT * FROM students;

# Запрос студентов старше 20 лет
SELECT * FROM students WHERE age > 20;

# Создание процедуры для выборки студентов с чётным id
CREATE PROCEDURE GetStudentsWithEvenId()
BEGIN
    SELECT * FROM students WHERE id % 2 = 0;
END;

# Вызов процедуры
CALL GetStudentsWithEvenId();