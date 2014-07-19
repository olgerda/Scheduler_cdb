CREATE DATABASE  IF NOT EXISTS `kvartet_new` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `kvartet_new`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: kvartetDBserver    Database: kvartet_new
-- ------------------------------------------------------
-- Server version	5.6.19-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cabinet`
--

DROP TABLE IF EXISTS `cabinet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cabinet` (
  `idcabinet` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `availability` int(10) unsigned zerofill NOT NULL,
  PRIMARY KEY (`idcabinet`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `clients` (
  `idclients` int(11) NOT NULL AUTO_INCREMENT,
  `name` tinytext,
  `comment` text,
  `blacklisted` tinyint(3) unsigned zerofill DEFAULT NULL,
  `price` int(10) unsigned zerofill NOT NULL,
  PRIMARY KEY (`idclients`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `receptions`
--

DROP TABLE IF EXISTS `receptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `receptions` (
  `idreceptions` int(11) NOT NULL AUTO_INCREMENT,
  `clientid` int(11) unsigned zerofill NOT NULL DEFAULT '00000000000',
  `specialistid` int(11) unsigned zerofill NOT NULL DEFAULT '00000000000',
  `cabinetid` int(11) unsigned zerofill NOT NULL DEFAULT '00000000000',
  `specializationid` int(11) unsigned zerofill NOT NULL DEFAULT '00000000000',
  `isrented` tinyint(3) unsigned zerofill NOT NULL DEFAULT '000',
  `timestart` time NOT NULL,
  `timeend` time NOT NULL,
  `timedate` date NOT NULL,
  PRIMARY KEY (`idreceptions`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `specialist2clientprice`
--

DROP TABLE IF EXISTS `specialist2clientprice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specialist2clientprice` (
  `idspecialist2clientcost` int(11) NOT NULL,
  `specid` int(11) NOT NULL,
  `clid` int(11) NOT NULL,
  `price` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  PRIMARY KEY (`idspecialist2clientcost`),
  KEY `specidkey_idx` (`specid`),
  KEY `clidkey_idx` (`clid`),
  CONSTRAINT `specidkey2` FOREIGN KEY (`specid`) REFERENCES `specialists` (`idspecialists`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `clidkey2` FOREIGN KEY (`clid`) REFERENCES `clients` (`idclients`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `specialists`
--

DROP TABLE IF EXISTS `specialists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specialists` (
  `idspecialists` int(11) NOT NULL AUTO_INCREMENT,
  `name` text,
  `notworking` tinyint(3) unsigned zerofill NOT NULL,
  PRIMARY KEY (`idspecialists`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `specializations`
--

DROP TABLE IF EXISTS `specializations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specializations` (
  `idspecializations` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idspecializations`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `specializations2specialist`
--

DROP TABLE IF EXISTS `specializations2specialist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specializations2specialist` (
  `idspecializations2specialist` int(11) NOT NULL AUTO_INCREMENT,
  `specialization` int(11) NOT NULL,
  `specialist` int(11) NOT NULL,
  PRIMARY KEY (`idspecializations2specialist`),
  KEY `specializationkey_idx` (`specialization`),
  KEY `specialistkey_idx` (`specialist`),
  CONSTRAINT `specialistkey` FOREIGN KEY (`specialist`) REFERENCES `specialists` (`idspecialists`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `specializationkey` FOREIGN KEY (`specialization`) REFERENCES `specializations` (`idspecializations`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `telephones`
--

DROP TABLE IF EXISTS `telephones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `telephones` (
  `idtelephones` int(11) NOT NULL AUTO_INCREMENT,
  `telephonescol` varchar(20) NOT NULL,
  PRIMARY KEY (`idtelephones`),
  UNIQUE KEY `idtelephones_UNIQUE` (`idtelephones`),
  UNIQUE KEY `telephonescol_UNIQUE` (`telephonescol`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `telephones2clients`
--

DROP TABLE IF EXISTS `telephones2clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `telephones2clients` (
  `idtelephones2clients` int(11) NOT NULL AUTO_INCREMENT,
  `telid` int(11) DEFAULT NULL,
  `clid` int(11) DEFAULT NULL,
  PRIMARY KEY (`idtelephones2clients`),
  KEY `telid2_idx` (`telid`),
  KEY `clientidkey_idx` (`clid`),
  CONSTRAINT `clidkey` FOREIGN KEY (`clid`) REFERENCES `clients` (`idclients`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `telidkey` FOREIGN KEY (`telid`) REFERENCES `telephones` (`idtelephones`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines for database 'kvartet_new'
--
/*!50003 DROP PROCEDURE IF EXISTS `CleanupTelephones` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = latin1 */ ;
/*!50003 SET character_set_results = latin1 */ ;
/*!50003 SET collation_connection  = latin1_swedish_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`kvartetAdmin`@`%` PROCEDURE `CleanupTelephones`()
BEGIN

DECLARE finish INTEGER DEFAULT 0;

DECLARE currentTelId INTEGER;

DECLARE idcount INTEGER DEFAULT 0;

DECLARE telCursor CURSOR FOR

SELECT idtelephones FROM telephones;

DECLARE CONTINUE HANDLER FOR NOT FOUND SET finish = 1;

OPEN telCursor;

fetch telCursor into currentTelId;

WHILE finish = 0 DO

SELECT COUNT(*) INTO idcount FROM telephones2clients WHERE telid = currentTelId;

IF idcount = 0 then

DELETE FROM telephones WHERE idtelephones = currentTelId;

END IF;

fetch telCursor into currentTelId;

END while;

CLOSE telCursor;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2014-07-19 17:52:46
