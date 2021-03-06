DROP DATABASE dbAppPerfin;
CREATE DATABASE dbAppPerFin;
USE dbAppPerFin;

CREATE TABLE SituacaoOperacao (
        idSituacaoOperacao INT AUTO_INCREMENT PRIMARY KEY,
        descricao VARCHAR(40) UNIQUE NOT NULL,
        sigla VARCHAR(2) UNIQUE NOT NULL
);
CREATE TABLE TipoOperacao (
        idTipoOperacao INT AUTO_INCREMENT PRIMARY KEY,
        descricao VARCHAR(40) UNIQUE NOT NULL,
        sigla VARCHAR(4) UNIQUE NOT NULL
);
CREATE TABLE FormaPagamento (
        idFormaPagamento INT AUTO_INCREMENT PRIMARY KEY,
        descricao VARCHAR(40) UNIQUE NOT NULL,
        sigla VARCHAR(4) UNIQUE NOT NULL
);
CREATE TABLE Categoria (
        idCategoria INT AUTO_INCREMENT PRIMARY KEY,
        descricao VARCHAR(40) UNIQUE NOT NULL,
        sigla VARCHAR(4) UNIQUE NOT NULL
);
CREATE TABLE Conta (
        idConta INT AUTO_INCREMENT PRIMARY KEY,
        descricao VARCHAR(50) UNIQUE NOT NULL,
        banco VARCHAR(20) NOT NULL,
        agencia VARCHAR(10) NOT NULL,
        conta VARCHAR(10) NOT NULL,
        credito TINYINT NOT NULL,
        debito TINYINT NOT NULL,
        transferencia TINYINT NOT NULL,
        
        CONSTRAINT UC_ContaBanco UNIQUE (banco, agencia, conta)
);
CREATE TABLE Transferencia (
        motivo VARCHAR(50),
        dtOperacao DATETIME NOT NULL,
        dtRegistro DATETIME NOT NULL,
        valor DECIMAL(12,4) NOT NULL,
        fkSituacaoOperacao INT NOT NULL,
        fkContaOrigem INT NOT NULL,
        fkContaDestino INT NOT NULL,
        
        FOREIGN KEY (fkSituacaoOperacao) REFERENCES SituacaoOperacao(idSituacaoOperacao),
        FOREIGN KEY (fkContaOrigem) REFERENCES Conta(idConta),
        FOREIGN KEY (fkContaDestino) REFERENCES Conta(idConta)
);
CREATE TABLE Titulo (
        idTitulo INT AUTO_INCREMENT PRIMARY KEY,
        numeroTitulo INT NOT NULL,
        numeroTituloSeq INT NOT NULL,
        fkTipoOperacao INT NOT NULL,
        entidade VARCHAR(120) NOT NULL,
        valor DECIMAL(12,4) NOT NULL,
        dtRegistro DATETIME NOT NULL,
        dtVencimento DATE NOT NULL,
        dtOperacao DATE NOT NULL,
        dtAlteracao DATETIME NOT NULL,
        fkCategoria INT NOT NULL,
        fkFormaPagamento INT NOT NULL,
        fkConta INT NOT NULL,
        
        FOREIGN KEY (fkTipoOperacao) REFERENCES TipoOperacao(idTipoOperacao),
        FOREIGN KEY (fkCategoria) REFERENCES Categoria(idCategoria),
        FOREIGN KEY (fkFormaPagamento) REFERENCES FormaPagamento(idFormaPagamento),
        FOREIGN KEY (fkConta) REFERENCES Conta(idConta),
        
        CONSTRAINT UC_Titulo UNIQUE (numeroTitulo, numeroTituloSeq)
);