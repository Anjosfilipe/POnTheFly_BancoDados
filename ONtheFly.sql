CREATE DATABASE OnTheFly;

USE OnTheFly;



CREATE TABLE Passageiro
(

Nome varchar(50) NOT NULL,
CPF varchar(11) NOT NULL,
DataNascimento varchar(10),
Sexo char(1),
UltimaCompra varchar(10),
DataCadatro varchar(10),
Situacao char(1),

CONSTRAINT PK_Passageiro PRIMARY KEY(CPF)
);

CREATE TABLE Compania_Aerea
(
RazaoSocial varchar(50)NOT NULL,
CNPJ varchar(11) NOT NULL,
DataAbertura varchar(10),
UltimoVoo varchar(10),
DataCadatro varchar(10),
Situacao char(1),

CONSTRAINT PK_Compania_Aerea PRIMARY KEY(CNPJ)
);

CREATE TABLE Aeronave
(
Inscricao varchar(6) NOT NULL,
Capacidade int NOT NULL,
UltimaVenda varchar(10),
DataCadastro varchar(10),
Situacao char(1),
CNPJ varchar(11),

CONSTRAINT PK_Aeronave PRIMARY KEY(Inscricao),
FOREIGN KEY(CNPJ) REFERENCES Compania_Aerea(CNPJ)

);

CREATE TABLE Voo
(
ID_Voo varchar(5) NOT NULL,
Destino varchar(3) NOT NULL,
Aeronave_Id varchar(6) NOT NULL,
DataVoo varchar(16) NOT NULL,
DataCadastro varchar(10) NOT NULL,
Situacao char(1),
AssentosOcupados int,

CONSTRAINT PK_Voo PRIMARY KEY(ID_Voo),
FOREIGN KEY(Aeronave_Id ) REFERENCES Aeronave(Inscricao)
);


CREATE TABLE PassagemVoo
(
ID_PassagemVoo varchar(6) NOT NULL,
ID_Voo varchar(5) NOT NULL,
DataUltima_Operacao varchar(10) NOT NULL,
Valor varchar(6) NOT NULL,
Situacao char(1) NOT NULL,

CONSTRAINT PK_PassagemVoo PRIMARY KEY(ID_PassagemVoo),
FOREIGN KEY(ID_Voo) REFERENCES Voo(ID_Voo)
);

CREATE TABLE Venda
(
ID_Venda int identity NOT NULL,
DataVenda varchar(10) NOT NULL,
CPF varchar(11) NOT NULL,
ValorTotal varchar(7)NULL,


CONSTRAINT PK_Venda PRIMARY KEY(ID_Venda),
FOREIGN KEY(CPF) REFERENCES Passageiro(CPF)
);

CREATE TABLE ItemVenda
(
ID_Venda int identity NOT NULL,
ID_PassagemVoo varchar(6) NOT NULL,
ValorUnitario varchar(6)NOT NULL

CONSTRAINT PK_ItemVenda PRIMARY KEY(ID_Venda,ID_PassagemVoo),
FOREIGN KEY(ID_Venda) REFERENCES Venda(ID_Venda ),
FOREIGN KEY(ID_PassagemVoo) REFERENCES PassagemVoo(ID_PassagemVoo)
);

CREATE Table IATA
(
IATA varchar(3) NOT NULL

CONSTRAINT PK_IATA PRIMARY KEY(IATA)
);

CREATE Table Restritos
(
CPF varchar(11) NOT NULL

CONSTRAINT PK_Restritos PRIMARY KEY(CPF)
);

CREATE Table Bloqueados
(
CNPJ varchar(14) NOT NULL

CONSTRAINT PK_Bloquados PRIMARY KEY(CNPJ)
);

select* from Compania_Aerea
select* from PassagemVoo
select* from Passageiro
select* from IATA
select* from Voo
select* from Venda
select* from Aeronave
UPDATE  PassagemVoo SET Situacao = 'L' WHERE Situacao = 'P' AND ID_Voo = 2