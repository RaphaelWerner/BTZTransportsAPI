CREATE TABLE Usuarios (
    id INT PRIMARY KEY IDENTITY(1,1),
    usuario NVARCHAR(50) NOT NULL,
    senha NVARCHAR(100) NOT NULL
);
insert into Usuarios (usuario, senha) values('usuario', 'senha123')

CREATE TABLE CategoriasCNH (
    id INT PRIMARY KEY IDENTITY(1,1),
    categoria Char NOT NULL
);
INSERT INTO CategoriasCNH (categoria)
VALUES 
('A'),
('B'),
('C'),
('D'),
('E');

CREATE TABLE Combustiveis (
    id INT PRIMARY KEY IDENTITY(1,1),
    tipo NVARCHAR(50) NOT NULL,
	preco DECIMAL(10, 2) NOT NULL
);
INSERT INTO Combustiveis (tipo, preco)
VALUES 
('Gasolina', 4.29),
('Etanol', 2.99),
('Diesel', 2.09);

CREATE TABLE Motoristas (
    id INT PRIMARY KEY IDENTITY(1,1),
    nome NVARCHAR(100) NOT NULL,
    cpf CHAR(11) NOT NULL,
    numeroCNH VARCHAR(20) NOT NULL,
    categoriaCNH_id INT NOT NULL,
    data_nascimento DATE NOT NULL,
    status BIT,
    FOREIGN KEY (categoriaCNH_id) REFERENCES CategoriasCNH(id)
);

CREATE TABLE Veiculos (
    id INT PRIMARY KEY IDENTITY(1,1),
    placa NVARCHAR(10) NOT NULL,
    nome NVARCHAR(100) NOT NULL,
    tipo_combustivel_id INT NOT NULL,
    fabricante NVARCHAR(50) NOT NULL,
    ano_fabricacao INT NOT NULL,
    capacidade_tanque DECIMAL(10, 2) NOT NULL,
    observacoes NVARCHAR(MAX), -- Campo de observações (não obrigatório)
    FOREIGN KEY (tipo_combustivel_id) REFERENCES Combustiveis(id)
);

CREATE TABLE Abastecimentos (
    id INT PRIMARY KEY IDENTITY(1,1),
    veiculo_id INT NOT NULL,
    motorista_id INT NOT NULL,
    data_abastecimento DATE NOT NULL,
    tipo_combustivel_id INT NOT NULL,
    quantidade_abastecida DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (veiculo_id) REFERENCES veiculos(id),
    FOREIGN KEY (motorista_id) REFERENCES motoristas(id),
    FOREIGN KEY (tipo_combustivel_id) REFERENCES combustiveis(id)
);