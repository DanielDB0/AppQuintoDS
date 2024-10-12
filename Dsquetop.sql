drop database dbDSquetop;
create database dbDSquetop;
use dbDSquetop;

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

Delimiter $$
create procedure spAttUser(vId int, vnome varchar(50), vCargo varchar(50), vDataNasc varchar(100))
begin
	update tbUsuario set nomeUsu = vnome, Cargo = vCargo, DataNasc = str_to_date(vDataNasc, '%d/%m/%Y')  where IdUsu = vId;
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

-- ------------------------------------------------------------- Cliente ---------------------------------------------------------------------

create table tbEndereco(
	Id int auto_increment primary key,
    CEP decimal(8,0)
);

create table tbCliente(
	IdCli int primary key auto_increment,
    nomeCli varchar(50) not null,
    DataRegistro datetime not null,
    CEP int not null,
    Telefone decimal(11,0) not null,
    constraint Fk_Cliente_Endereco foreign key (CEP) references tbEndereco(Id)
);


DELIMITER $$
create procedure spCadastrarCli(vNome varchar(50), vCEP decimal(8,0), vTelefone decimal(11,0))
begin
	if (not exists (select Id from tbEndereco where CEP = vCEP)) then
		insert into tbEndereco (CEP) values (vCEP);
    end if;
	insert into tbCliente (nomeCli, CEP, Telefone, DataRegistro) values (vNome, (select Id from tbEndereco where CEP = vCEP), vTelefone, current_date());
end $$ 


Delimiter $$
create procedure spAttCli(vId int, vNome varchar(50), vCEP decimal(8,0), vTelefone decimal(11,0))
begin
	if (not exists (select Id from tbEndereco where CEP = vCEP)) then
		insert into tbEndereco (CEP) values (vCEP);
    end if;

	update tbCliente set nomeCli = vNome, CEP = (select Id from tbEndereco where CEP = vCEP), Telefone = vTelefone where IdCli = vId;
end $$

DELIMITER $$
create procedure spExcluirCli(vId int)
begin
	 delete from tbCliente where IdCli = vId;
end $$ 

Delimiter $$

create procedure spObterClient(vId int)
BEGIN
	select tbCliente.IdCli, tbCliente.nomeCli,tbCliente.DataRegistro, tbCliente.Telefone, tbEndereco.CEP from tbCliente inner join tbEndereco on tbCliente.CEP = tbEndereco.Id where tbCliente.Id = vId;
END $$

Delimiter $$
create procedure spObterAllClients()
BEGIN
	select tbCliente.IdCli, tbCliente.nomeCli,tbCliente.DataRegistro, tbCliente.Telefone, tbEndereco.CEP from tbCliente inner join tbEndereco on tbCliente.CEP = tbEndereco.Id;
END $$


describe tbCliente