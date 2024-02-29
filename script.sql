create table AspNetRoles
(
    Id               nvarchar(450) not null
        constraint PK_AspNetRoles
            primary key,
    Name             nvarchar(256),
    NormalizedName   nvarchar(256),
    ConcurrencyStamp nvarchar(max)
)
go

create table AspNetRoleClaims
(
    Id         int identity
        constraint PK_AspNetRoleClaims
            primary key,
    RoleId     nvarchar(450) not null
        constraint FK_AspNetRoleClaims_AspNetRoles_RoleId
            references AspNetRoles
            on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)
go

create index IX_AspNetRoleClaims_RoleId
    on AspNetRoleClaims (RoleId)
go

create unique index RoleNameIndex
    on AspNetRoles (NormalizedName)
    where [NormalizedName] IS NOT NULL
go

create table AspNetUsers
(
    Id                   nvarchar(450) not null
        constraint PK_AspNetUsers
            primary key,
    Nome                 nvarchar(max) not null,
    Administrador        bit           not null,
    UserName             nvarchar(256),
    NormalizedUserName   nvarchar(256),
    Email                nvarchar(256),
    NormalizedEmail      nvarchar(256),
    EmailConfirmed       bit           not null,
    PasswordHash         nvarchar(max),
    SecurityStamp        nvarchar(max),
    ConcurrencyStamp     nvarchar(max),
    PhoneNumber          nvarchar(max),
    PhoneNumberConfirmed bit           not null,
    TwoFactorEnabled     bit           not null,
    LockoutEnd           datetimeoffset,
    LockoutEnabled       bit           not null,
    AccessFailedCount    int           not null
)
go

create table AspNetUserClaims
(
    Id         int identity
        constraint PK_AspNetUserClaims
            primary key,
    UserId     nvarchar(450) not null
        constraint FK_AspNetUserClaims_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    ClaimType  nvarchar(max),
    ClaimValue nvarchar(max)
)
go

create index IX_AspNetUserClaims_UserId
    on AspNetUserClaims (UserId)
go

create table AspNetUserLogins
(
    LoginProvider       nvarchar(450) not null,
    ProviderKey         nvarchar(450) not null,
    ProviderDisplayName nvarchar(max),
    UserId              nvarchar(450) not null
        constraint FK_AspNetUserLogins_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    constraint PK_AspNetUserLogins
        primary key (LoginProvider, ProviderKey)
)
go

create index IX_AspNetUserLogins_UserId
    on AspNetUserLogins (UserId)
go

create table AspNetUserRoles
(
    UserId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    RoleId nvarchar(450) not null
        constraint FK_AspNetUserRoles_AspNetRoles_RoleId
            references AspNetRoles
            on delete cascade,
    constraint PK_AspNetUserRoles
        primary key (UserId, RoleId)
)
go

create index IX_AspNetUserRoles_RoleId
    on AspNetUserRoles (RoleId)
go

create table AspNetUserTokens
(
    UserId        nvarchar(450) not null
        constraint FK_AspNetUserTokens_AspNetUsers_UserId
            references AspNetUsers
            on delete cascade,
    LoginProvider nvarchar(450) not null,
    Name          nvarchar(450) not null,
    Value         nvarchar(max),
    constraint PK_AspNetUserTokens
        primary key (UserId, LoginProvider, Name)
)
go

create index EmailIndex
    on AspNetUsers (NormalizedEmail)
go

create unique index UserNameIndex
    on AspNetUsers (NormalizedUserName)
    where [NormalizedUserName] IS NOT NULL
go

create table MSreplication_options
(
    optname          sysname not null,
    value            bit     not null,
    major_version    int     not null,
    minor_version    int     not null,
    revision         int     not null,
    install_failures int     not null
)
go

create table PessoasFisicas
(
    Id             int identity
        constraint PK_PessoasFisicas
            primary key,
    Nome           nvarchar(max) not null,
    DataNascimento datetime      not null,
    Cpf            nvarchar(max) not null,
    Rg             nvarchar(max) not null
)
go

create table Colaboradores
(
    Id                int           not null
        constraint PK_Colaboradores
            primary key
        constraint FK_Colaboradores_PessoasFisicas_Id
            references PessoasFisicas
            on delete cascade,
    Matricula         nvarchar(max) not null,
    Tipo              int           not null,
    DataAdmissao      datetime,
    ValorContribuicao decimal(10, 2)
)
go

create table Enderecos
(
    Id                int identity
        constraint PK_Enderecos
            primary key,
    Tipo              int           not null,
    Cep               nvarchar(max) not null,
    Logradouro        nvarchar(max) not null,
    NumeroComplemento nvarchar(max) not null,
    Bairro            nvarchar(max) not null,
    Cidade            nvarchar(max) not null,
    PessoaFisicaId    int           not null
        constraint FK_Enderecos_PessoasFisicas_PessoaFisicaId
            references PessoasFisicas
            on delete cascade
)
go

create index IX_Enderecos_PessoaFisicaId
    on Enderecos (PessoaFisicaId)
go

create table Telefones
(
    Id             int identity
        constraint PK_Telefones
            primary key,
    Tipo           int           not null,
    Numero         nvarchar(max) not null,
    PessoaFisicaId int           not null
        constraint FK_Telefones_PessoasFisicas_PessoaFisicaId
            references PessoasFisicas
            on delete cascade
)
go

create index IX_Telefones_PessoaFisicaId
    on Telefones (PessoaFisicaId)
go

create table Usuarios
(
    Id                   nvarchar(450) not null
        constraint PK_Usuarios
            primary key,
    Nome                 nvarchar(max) not null,
    Administrador        bit           not null,
    UserName             nvarchar(max),
    NormalizedUserName   nvarchar(max),
    Email                nvarchar(max),
    NormalizedEmail      nvarchar(max),
    EmailConfirmed       bit           not null,
    PasswordHash         nvarchar(max),
    SecurityStamp        nvarchar(max),
    ConcurrencyStamp     nvarchar(max),
    PhoneNumber          nvarchar(max),
    PhoneNumberConfirmed bit           not null,
    TwoFactorEnabled     bit           not null,
    LockoutEnd           datetimeoffset,
    LockoutEnabled       bit           not null,
    AccessFailedCount    int           not null
)
go

create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go

create table spt_fallback_db
(
    xserver_name       varchar(30) not null,
    xdttm_ins          datetime    not null,
    xdttm_last_ins_upd datetime    not null,
    xfallback_dbid     smallint,
    name               varchar(30) not null,
    dbid               smallint    not null,
    status             smallint    not null,
    version            smallint    not null
)
go

grant select on spt_fallback_db to [public]
go

create table spt_fallback_dev
(
    xserver_name       varchar(30)  not null,
    xdttm_ins          datetime     not null,
    xdttm_last_ins_upd datetime     not null,
    xfallback_low      int,
    xfallback_drive    char(2),
    low                int          not null,
    high               int          not null,
    status             smallint     not null,
    name               varchar(30)  not null,
    phyname            varchar(127) not null
)
go

grant select on spt_fallback_dev to [public]
go

create table spt_fallback_usg
(
    xserver_name       varchar(30) not null,
    xdttm_ins          datetime    not null,
    xdttm_last_ins_upd datetime    not null,
    xfallback_vstart   int,
    dbid               smallint    not null,
    segmap             int         not null,
    lstart             int         not null,
    sizepg             int         not null,
    vstart             int         not null
)
go

grant select on spt_fallback_usg to [public]
go

create table spt_monitor
(
    lastrun       datetime not null,
    cpu_busy      int      not null,
    io_busy       int      not null,
    idle          int      not null,
    pack_received int      not null,
    pack_sent     int      not null,
    connections   int      not null,
    pack_errors   int      not null,
    total_read    int      not null,
    total_write   int      not null,
    total_errors  int      not null
)
go

grant select on spt_monitor to [public]
go


