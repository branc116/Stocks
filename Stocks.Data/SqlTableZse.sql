create table DionickoDrustvo(
    idDD integer primary key,
    oznakaDD char(20),
    imeDD char(30)
);
create table Sektor (
    idSektor integer primary key,
    oznSektor char(3),
    nazivSektor char(50)
);
create table DionickoDrustvoDjelatnost(
    idDjelatnost integer references Djelatnost(idDjelatnost),
    idDD integer references DionickoDrustvo(idDD),
    constraint pk_0 primary key(idDD)
);
create table Djelatnost (
    idDjelatnost integer,
    oznDjelatnost char(10),
    nazivDjelatnost char(50),
    idSektor integer references Sektor(idSektor)
)
create table DionickoDrustvoKategorijaLikvidnosti (
    idDD integer references DionickoDrustvo(idDD),
    datum date,
    vrijednost smallint,
    constraint pk_2 primary key(datum, idDD)
);
create table DnevnoTrgovanje(
    datum date,
    idDD integer references DionickoDrustvo(idDD),
    prva decimal(2),
    najniza decimal(2),
	najvisa decimal(2),
	zadnja decimal(2),
	prosijek decimal(2),
    promijena decimal(2),
	brojTransakcija integer,
	promet integer,
    constraint pk_1 primary key(datum, idDD)
);
create table Dionicar (
	idDionicar int identity(1,1) primary key,
	nazivDionicar char(50)
);
create table DionicarDionickoDrustvo (
	idDionicar int references Dionicar(idDionicar),
	idDD integer references DionickoDrustvo(idDD),
	datum date,
	postoDionica decimal(2),
	constraint pk_3 primary key(rbrDionicar, oznakaDrustvo, datum)
);