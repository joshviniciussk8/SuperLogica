Create Database SuperLogica;
use SuperLogica;

CREATE TABLE Contato (
    ContatoID INT IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
	Celular NVARCHAR(20) PRIMARY KEY,
    Email NVARCHAR(255),    
	Logradouro NVARCHAR(255),
    Numero NVARCHAR(20),
	Complemento NVARCHAR(100),
	Bairro NVARCHAR(50),
	Cidade NVARCHAR(50),
	Estado NVARCHAR(50),
    Cep NVARCHAR(10),
	Pais NVARCHAR(50)
);

select * from Contato;

select * from Contato where Email = 'email@example.com' and Celular = '123456789';

delete from Contato where ContatoID > 1

insert into Contato(Nome,Email,Celular,Endereco,Numero,Cep)values('Josh Vinicius Sousa de Lima', 'joshviniciussk11@gmail.com','11986046374','Padre Montoya',18,'08110600')


INSERT INTO Contato (
    Nome,
    Email,
	Celular,
    Logradouro,
    Numero,
    Complemento,
    Bairro,
    Cidade,
    Estado,
    CEP,
    Pais
)
VALUES (
    'Sara Sena',
	'SaraSen7a@gmail.com',
	'119899889967',
    'Rua Principal',
    '123',
    'Apto 45',
    'Centro',
    'São Paulo',
    'SP',
    '01234-567',
    'Brasil'
);

update contato set Nome = '', Email='',Celular='',Logradouro='',Numero='',Complemento='',Bairro='',Cidade='',Estado='',CEP='',Pais='' where Celular = ''




Nome, Email, Celular, Logradouro, Numero, Complemento, Bairro, Cidade, Estado, CEP,  Pais