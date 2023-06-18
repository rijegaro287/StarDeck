create table if not exists card
(
    id          char(14)             not null,
    name        varchar(30)          not null,
    energy      integer default 0    not null,
    battlecost  integer default 0    not null,
    image       bytea                not null,
    active      boolean default true not null,
    type        integer default 0    not null,
    "ability "  json,
    description text,
    race        text
);

comment on table card is '0-basica

1-normal

2-rara

3-Muy Rara

4-Ultra Rara';

alter table card
    owner to "StardeckAdmin";

alter table card
    add constraint card_pkey
        primary key (id);

alter table card
    add constraint card_energy_check
        check ((energy < 101) AND (energy > '-101'::integer));

alter table card
    add constraint card_battlecost_check
        check ((battlecost >= 0) AND (battlecost <= 100));

alter table card
    add constraint card_type_check
        check ((type >= 0) AND (type <= 4));

create table if not exists account
(
    id          char(14)             not null,
    name        varchar(30)          not null,
    nickname    text                 not null,
    email       text                 not null,
    country     text                 not null,
    password    text                 not null,
    active      boolean default true not null,
    avatar      bigint  default 0    not null,
    config      json,
    points      bigint  default 0    not null,
    coins       bigint  default 20   not null,
    gamecounter bigint  default 0
);

alter table account
    owner to "StardeckAdmin";

alter table account
    add constraint account_pkey
        primary key (id);

alter table account
    add constraint account_nickname_key
        unique (nickname);

alter table account
    add constraint account_email_key
        unique (email);

create table if not exists avatar
(
    id    bigint not null,
    image bytea  not null,
    name  text   not null
);

alter table avatar
    owner to "StardeckAdmin";

alter table avatar
    add constraint avatar_pkey
        primary key (id);

alter table account
    add constraint account_avatar_fkey
        foreign key (avatar) references avatar;

create table if not exists collection
(
    id_account char(14) not null,
    collection char(14)[]
);

alter table collection
    owner to "StardeckAdmin";

alter table collection
    add constraint deck_pkey
        primary key (id_account);

alter table collection
    add constraint deck_account_fkey
        foreign key (id_account) references account;

create table if not exists avatar_account
(
    id_account char(14) not null,
    avatar     bigint   not null
);

alter table avatar_account
    owner to "StardeckAdmin";

alter table avatar_account
    add constraint avatar_account_pkey
        primary key (id_account, avatar);

alter table avatar_account
    add constraint avatar_account_avatar_fkey
        foreign key (avatar) references avatar;

alter table avatar_account
    add constraint avatar_account_id_account_fkey
        foreign key (id_account) references account;

create table if not exists deck
(
    id_account char(14)   not null,
    cardlist   char(14)[] not null,
    id_deck    char(14)   not null,
    "deckName" char(14)
);

alter table deck
    owner to "StardeckAdmin";

alter table deck
    add constraint deck_pkey1
        primary key (id_deck);

alter table deck
    add constraint "deck_deckName_deckName1_key"
        unique ("deckName");

alter table deck
    add constraint deck_id_account_fkey
        foreign key (id_account) references account;

create table if not exists gameroom
(
    roomid  char(14) not null,
    player1 char(14) not null,
    player2 char(14) not null,
    winner  char(14),
    bet     bigint
);

alter table gameroom
    owner to "StardeckAdmin";

alter table gameroom
    add constraint gameroom_pkey
        primary key (roomid);

alter table gameroom
    add constraint gameroom_player1_fkey
        foreign key (player1) references account;

alter table gameroom
    add constraint gameroom_player2_fkey
        foreign key (player2) references account;

alter table gameroom
    add constraint gameroom_winner_fkey
        foreign key (winner) references account;

create table if not exists gamelog
(
    gameid char(14) not null,
    log    text
);

alter table gamelog
    owner to "StardeckAdmin";

alter table gamelog
    add constraint gamelog_pkey
        primary key (gameid);

alter table gamelog
    add constraint gamelog_gameid_fkey
        foreign key (gameid) references gameroom;

create table if not exists favorite_deck
(
    accountid char(14) not null,
    deckid    char(14)
);

alter table favorite_deck
    owner to "StardeckAdmin";

alter table favorite_deck
    add constraint favorite_deck_pkey
        primary key (accountid);

alter table favorite_deck
    add constraint favorite_deck_deckid_deckid1_key
        unique (deckid);

alter table favorite_deck
    add constraint favorite_deck_accountid_fkey
        foreign key (accountid) references account;

alter table favorite_deck
    add constraint favorite_deck_deckid_fkey
        foreign key (deckid) references deck;

create table if not exists planet
(
    id          char(14)             not null,
    name        varchar(30)          not null,
    type        bigint  default 0    not null,
    active      boolean default true not null,
    description text,
    ability     json,
    image       bytea
);

comment on table planet is '0 raro
1 basico
2 popular';

alter table planet
    owner to "StardeckAdmin";

alter table planet
    add constraint planet_pkey
        primary key (id);

alter table planet
    add constraint planet_name_name1_key
        unique (name);

alter table planet
    add constraint planet_type_check
        check ((type >= 0) AND (type <= 2));

create table if not exists parameters
(
    key   text not null,
    value text
);

alter table parameters
    owner to "StardeckAdmin";

alter table parameters
    add constraint constants_pkey
        primary key (key);

create function update_gamecounter() returns trigger
    language plpgsql
as
$$
BEGIN
    UPDATE public.account
    SET gamecounter = gamecounter + 1
    WHERE id IN (NEW.player1, NEW.player2);

    RETURN NEW;
END;
$$;

alter function update_gamecounter() owner to "StardeckAdmin";

create trigger increment_gamecounter
    after insert
    on gameroom
    for each row
    when (new.player1 IS NOT NULL AND new.player2 IS NOT NULL)
execute procedure update_gamecounter();


