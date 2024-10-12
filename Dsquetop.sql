drop database dbDSquetop;
create database dbDSquetop;use dbDSquetop;

create table tbUsuario(
	IdUsu int primary key auto_increment,
    nomeUsu varchar(50) not null, 
    Cargo varchar(50) not null,
    DataNasc datetime
);

alter table TbUsuario modify DataNasc date not null;

DELIMITER $$

create procedure spEnviarDados(vnome varchar(50), vCargo varchar(50), vDataNasc varchar(100))
begin
	insert into tbUsuario (nomeUsu, Cargo, DataNasc) values (vNome, vCargo, str_to_date(vDataNasc, '%d/%m/%Y'));
end $$ 

DELIMITER $$
create procedure spExcluir(vId int)
begin
	 delete from tbUsuario where IdUsu = vId;
end $$ 

Delimiter $$

create procedure spObterUser(vIdUsu int)
BEGIN
	select * from tbusuario WHERE IdUsu = vIdUsu;
END $$

Delimiter $$
create procedure spObterAllUsers()
BEGIN
	select * from tbusuario;
END $$

create table tbEndereco(
	Id int auto_increment,
    CEP decimal(8,0)
);

create table tbCliente(
	IdCli int primary key auto_increment,
    nomeCli varchar(50) not null,
    DataRegistro datetime,
    CEP int
    Telefone decimal(11,0),
    constraint Fk_Cliente_Endereco foreign key (CEP) references tbEndereco(Id)
);



	select * from tbusuario;