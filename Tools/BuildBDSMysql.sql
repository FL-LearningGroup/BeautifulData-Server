/*Create bdstest databases*/
CREATE DATABASE IF NOT EXISTS bds CHARACTER SET utf8 COLLATE utf8_general_ci;

/*Create bds user*/
create user 'bdsuser'@'localhost' identified by 'Ya0di+2019';

/*Grant database privileges to user*/
grant all privileges ON bds.* to 'bdsuser'@'localhost' identified by 'Ya0di+2019';
grant all privileges ON bds.* to 'bdsuser'@'%' identified by 'Ya0di+2019';

/*Enable user remote access*/
grant all privileges on bds.* to 'bdsuser'@'%' identified by 'Ya0di+2019' with grant option;

/*Flush privileges table*/
flush privileges;

/*Check user privileges*/
show grants for bdsuser;