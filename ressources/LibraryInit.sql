
DROP TABLE IF EXISTS library;
CREATE TABLE IF NOT EXISTS library (
	id integer primary key autoincrement,
	name text,
	adress text
);

DROP TABLE IF EXISTS stock;
CREATE TABLE IF NOT EXISTS stock(
	id_library integer,
	id_book integer,
	foreign key (id_library) REFERENCES library(id),
	foreign key (id_book) REFERENCES book(id)
);

DROP TABLE IF EXISTS book;
CREATE TABLE IF NOT EXISTS book (
	id integer primary key autoincrement,
	name text,
	pages integer,
	type text,
	rate integer,
	id_author integer,
	foreign key (id_author) REFERENCES author(id)
);

DROP TABLE IF EXISTS author;
CREATE TABLE IF NOT EXISTS author (
	id integer primary key autoincrement,
	firstName text,
	lastName text
);




INSERT INTO library VALUES (1, "Bibliothèque Roubaix", "44 Av. Jean Lebas, 59100 Roubaix"),
	(2, "Médiathèque Calais", "16 Rue du Pont Lottin, 62100 Calais");
	
INSERT INTO author VALUES (1, "Alexandre", "Dumas"),
	(2, "Remy", "Synave"),
	(3, "Dany", "Capitaine"),
	(4, "Severine", "Lettrez");
	

INSERT INTO book VALUES (1, "Le conte de Monte Cristo", 900, "ADVENTURE", 10, 1),
	(2, "Les trois mousquetaires", 300, "ADVENTURE", 9, 1),
	(3, "Apprendre le Java mais pas sur l'île de Java", 900, "ENSEIGNEMENT", 10, 2),
	(4, "Le RC Lense, un club pas comme les autres", 900, "HISTOIRE", 10, 4),
	(5, "La RGPD, une protection contre l 'injustice", 900, "JURIDIQUE", 10, 1);