USE Video90s;
GO

-- 1) Usuarios
INSERT INTO [Users] ([UserName], [Email], [PasswordHash], [IsActive], [RegisteredAt], [Role]) VALUES
('Juan Pérez',           'juanperez@gmail.com',        '1234', 1, GETDATE(), 'User'),
('María García',         'mariagarcia@svalero.com',    '1234', 1, GETDATE(), 'User'),
('Luis Fernández',       'luisfernandez@hotmail.com',  '1234', 1, GETDATE(), 'User'),
('Ana Sánchez',          'anasanchez@yahoo.es',        '1234', 1, GETDATE(), 'User'),
('Carlos Rodríguez',     'carlosrod@gmail.com',        '1234', 1, GETDATE(), 'User'),
('Laura Martínez',       'lauramartinez@svalero.com',  '1234', 1, GETDATE(), 'User'),
('David López',          'davidlopez@hotmail.com',     '1234', 1, GETDATE(), 'User'),
('Elena Gómez',          'elenagomez@yahoo.es',         '1234', 1, GETDATE(), 'User'),
('Javier Hernández',     'javierhernandez@gmail.com',  '1234', 1, GETDATE(), 'User'),
('Marta Díaz',           'martadiaz@svalero.com',       '1234', 1, GETDATE(), 'User');
GO

-- 2) Películas
INSERT INTO [Movies] ([Title], [Genre], [ReleaseDate],      [Price], [IsAvailable], [Format]) VALUES
('The Matrix',               'Sci-Fi',    '1999-03-31',     9,    1,              'DVD'),
('Pulp Fiction',            'Crime',     '1994-10-14',     7,    1,              'Blu-Ray'),
('Forrest Gump',            'Drama',     '1994-07-06',     8,    1,              'DVD'),
('Gladiator',               'Action',    '2000-05-05',     6,    1,              'Blu-Ray'),
('The Shawshank Redemption','Drama',     '1994-09-22',     9,    1,              'DVD'),
('Titanic',                 'Romance',   '1997-12-19',     7,    1,              'Blu-Ray'),
('Inception',               'Sci-Fi',    '2010-07-16',    10,    1,              'Digital'),
('The Godfather',           'Crime',     '1972-03-24',     8,    1,              'DVD'),
('Jurassic Park',           'Adventure', '1993-06-11',     5,    1,              'DVD'),
('The Dark Knight',         'Action',    '2008-07-18',     9,    1,              'Blu-Ray'),
('Fight Club',              'Drama',     '1999-10-15',     6,    1,              'Digital'),
('La La Land',              'Musical',   '2016-12-09',     7,    1,              'Digital'),
-- videojuegos
('The Legend of Zelda',     'Adventure', '1986-02-21',    49,    1,              'Videojuego'),
('Super Mario Bros',        'Platformer','1985-09-13',    39,    1,              'Videojuego'),
('Minecraft',               'Sandbox',   '2011-11-18',    29,    1,              'Videojuego');
GO

-- 3) Alquileres
INSERT INTO [Rentals] ([UserId], [MovieId], [RentedAt],     [ReturnedAt], [IsReserved]) VALUES
(1,  1,  '2025-06-01', '2025-06-05', 0),
(2,  3,  '2025-06-02', '2025-06-04', 0),
(3,  5,  '2025-06-03', NULL,         1),
(4,  2,  '2025-06-04', '2025-06-10', 0),
(5,  4,  '2025-06-05', NULL,         1),
-- alquileres de videojuegos
(1, 13,  '2025-06-06', NULL,         0),
(2, 14,  '2025-06-07', NULL,         1);
GO
