/*Create bdstest databases*/
CREATE DATABASE IF NOT EXISTS bdstest
  CHARACTER SET utf8
  COLLATE utf8_general_ci;

/*Create bds user*/
create user 'bds'@'localhost' identified  by 'Ya0di+2019';

/*Grant database privileges to user*/
grant all privileges ON bdstest.* to bds@localhost identified  by 'Ya0di+2019';

/*Enable user remote access*/
grant all privileges on bdstest.* to 'bds'@'%' identified  by 'Ya0di+2019' with grant option;

/*Flush privileges table*/
flush privileges;

/*Check user privileges*/
show grants for bds;