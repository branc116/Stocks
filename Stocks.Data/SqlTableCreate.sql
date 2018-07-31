create table Drustvo(
	oznaka char(20) primary key,
	naziv char(50),
	mojedioniceUrl char(100)
);
create table PovijestTransakcija(
	datum datetime, 
	oznakaDrustvo char(20) references Drustvo(oznaka),
	prva decimal(2),
	zadnja decimal(2),
	prosijek decimal(2),
	kolicina integer,
	promet integer,
	najniza decimal(2),
	najvisa decimal(2),
	kupnja decimal(2),
	prodaja decimal(2),
	constraint pk_0 primary key (datum, oznakaDrustvo)
);
create table Dionicar (
	rbr int identity(1,1) primary key,
	naziv char(50)
);

create table DionicarDrustvo (
	rbrDionicar int references Dionicar(rbr),
	oznakaDrustvo char(20) references Drustvo(oznaka),
	datum datetime,
	brojDionica int,
	constraint pk_1 primary key(rbrDionicar, oznakaDrustvo, datum)
);
