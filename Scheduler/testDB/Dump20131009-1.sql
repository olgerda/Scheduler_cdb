CREATE DATABASE  IF NOT EXISTS `kvartet` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_bin */;
USE `kvartet`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: 192.168.159.132    Database: kvartet
-- ------------------------------------------------------
-- Server version	5.6.14-log

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
-- Table structure for table `cabinets`
--

DROP TABLE IF EXISTS `cabinets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cabinets` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `Name` mediumtext COLLATE utf8_bin NOT NULL,
  `Availability` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cabinets`
--

LOCK TABLES `cabinets` WRITE;
/*!40000 ALTER TABLE `cabinets` DISABLE KEYS */;
INSERT INTO `cabinets` VALUES (1,'ПервыйКабинет',1),(2,'ВторойКабинет',1),(3,'ТретийКабинет',0),(4,'ЧетвёртыйКабинет',1),(5,'111',1),(6,'123',1),(7,'122',1);
/*!40000 ALTER TABLE `cabinets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `clients` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `id_FIO` smallint(5) unsigned NOT NULL,
  `Comment` mediumtext COLLATE utf8_bin NOT NULL,
  `TelNumber` bigint(10) unsigned NOT NULL,
  `inRedList` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `id_FIO` (`id_FIO`),
  CONSTRAINT `clients_ibfk_1` FOREIGN KEY (`id_FIO`) REFERENCES `fio` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

LOCK TABLES `clients` WRITE;
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients` VALUES (1,7,'первый комментарий',9091234567,0),(2,8,'второй комментарий',9261234567,0),(3,9,'третий комментарий',9031234567,0);
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `clients_view`
--

DROP TABLE IF EXISTS `clients_view`;
/*!50001 DROP VIEW IF EXISTS `clients_view`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `clients_view` (
  `CName` tinyint NOT NULL,
  `CSurname` tinyint NOT NULL,
  `CPatronimyc` tinyint NOT NULL,
  `cl_id` tinyint NOT NULL,
  `Comment` tinyint NOT NULL,
  `TelNumber` tinyint NOT NULL,
  `inRedList` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `fio`
--

DROP TABLE IF EXISTS `fio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fio` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `Name` tinytext CHARACTER SET utf8 NOT NULL,
  `Surname` tinytext CHARACTER SET utf8 NOT NULL,
  `Patronimyc` tinytext CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fio`
--

LOCK TABLES `fio` WRITE;
/*!40000 ALTER TABLE `fio` DISABLE KEYS */;
INSERT INTO `fio` VALUES (1,'Ivan','Ivanov','Ivanovich'),(2,'Пётр','Петров','Петрович'),(3,'Сергей','Сергеев','Сергеевич'),(4,'Тест1имя','Тест1фамилия','Тест1отчество'),(5,'Тест2имя','Тест2фамилия','Тест2отчество'),(6,'Тест3имя','Тест3фамилия','Тест3отчество'),(7,'Тест4имя','Тест4фамилия','Тест4отчество'),(8,'Тест5имя','Тест5фамилия','Тест5отчество'),(9,'Тест6имя','Тест6фамилия','Тест6отчество'),(11,'Client1TestName','Client1TestSurname','Client1TestPatromnimyc');
/*!40000 ALTER TABLE `fio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `reception_view`
--

DROP TABLE IF EXISTS `reception_view`;
/*!50001 DROP VIEW IF EXISTS `reception_view`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `reception_view` (
  `CName` tinyint NOT NULL,
  `CSurname` tinyint NOT NULL,
  `CPatronimyc` tinyint NOT NULL,
  `cl_id` tinyint NOT NULL,
  `Comment` tinyint NOT NULL,
  `TelNumber` tinyint NOT NULL,
  `inRedList` tinyint NOT NULL,
  `SName` tinyint NOT NULL,
  `SSurname` tinyint NOT NULL,
  `SPatronimyc` tinyint NOT NULL,
  `spec_id` tinyint NOT NULL,
  `Specialization` tinyint NOT NULL,
  `CabName` tinyint NOT NULL,
  `startTime` tinyint NOT NULL,
  `endTime` tinyint NOT NULL,
  `receptionDate` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `receptioncards`
--

DROP TABLE IF EXISTS `receptioncards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `receptioncards` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `Client` smallint(5) unsigned NOT NULL,
  `Specialist` smallint(5) unsigned NOT NULL,
  `Specialization` tinyint(3) unsigned NOT NULL,
  `Cabinet` smallint(5) unsigned NOT NULL,
  `startTime` time NOT NULL DEFAULT '10:00:00',
  `endTime` time NOT NULL DEFAULT '12:00:00',
  `receptionDate` date NOT NULL DEFAULT '2013-10-07',
  PRIMARY KEY (`id`),
  KEY `Client` (`Client`),
  KEY `Specialist` (`Specialist`),
  KEY `Specialization` (`Specialization`),
  KEY `Cabinet` (`Cabinet`),
  CONSTRAINT `receptioncards_ibfk_1` FOREIGN KEY (`Client`) REFERENCES `clients` (`id`),
  CONSTRAINT `receptioncards_ibfk_2` FOREIGN KEY (`Specialist`) REFERENCES `specialist` (`id`),
  CONSTRAINT `receptioncards_ibfk_3` FOREIGN KEY (`Specialization`) REFERENCES `specializations` (`id`),
  CONSTRAINT `receptioncards_ibfk_4` FOREIGN KEY (`Cabinet`) REFERENCES `cabinets` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receptioncards`
--

LOCK TABLES `receptioncards` WRITE;
/*!40000 ALTER TABLE `receptioncards` DISABLE KEYS */;
INSERT INTO `receptioncards` VALUES (1,1,1,1,1,'10:00:00','12:00:00','2013-10-07'),(2,2,2,2,2,'12:12:00','13:30:00','2013-12-02');
/*!40000 ALTER TABLE `receptioncards` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specialist`
--

DROP TABLE IF EXISTS `specialist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specialist` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `id_FIO` smallint(5) unsigned NOT NULL,
  `SpecializationList` bit(64) NOT NULL DEFAULT b'0' COMMENT 'Contain bitfield of length 64 that represent numbers of tabl',
  PRIMARY KEY (`id`),
  KEY `id_FIO` (`id_FIO`),
  CONSTRAINT `specialist_ibfk_1` FOREIGN KEY (`id_FIO`) REFERENCES `fio` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specialist`
--

LOCK TABLES `specialist` WRITE;
/*!40000 ALTER TABLE `specialist` DISABLE KEYS */;
INSERT INTO `specialist` VALUES (1,1,'\0\0\0\0\0\0\0'),(2,2,'\0\0\0\0\0\0\0'),(3,3,'\0\0\0\0\0\0\0'),(4,4,'\0\0\0\0\0\0\0'),(5,5,'\0\0\0\0\0\0\0'),(6,6,'\0\0\0\0\0\0\0');
/*!40000 ALTER TABLE `specialist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specializations`
--

DROP TABLE IF EXISTS `specializations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specializations` (
  `id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `Specialization` mediumtext COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specializations`
--

LOCK TABLES `specializations` WRITE;
/*!40000 ALTER TABLE `specializations` DISABLE KEYS */;
INSERT INTO `specializations` VALUES (1,'Специализация1'),(2,'Специализация2'),(3,'Хирург'),(4,'Офтальмолог'),(5,'Ортопед'),(6,'Педиатр'),(7,'ЛОР');
/*!40000 ALTER TABLE `specializations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `specs_view`
--

DROP TABLE IF EXISTS `specs_view`;
/*!50001 DROP VIEW IF EXISTS `specs_view`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `specs_view` (
  `SName` tinyint NOT NULL,
  `SSurname` tinyint NOT NULL,
  `SPatronimyc` tinyint NOT NULL,
  `spec_id` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Dumping routines for database 'kvartet'
--

--
-- Final view structure for view `clients_view`
--

/*!50001 DROP TABLE IF EXISTS `clients_view`*/;
/*!50001 DROP VIEW IF EXISTS `clients_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`kvartetAdmin`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `clients_view` AS select `fio`.`Name` AS `CName`,`fio`.`Surname` AS `CSurname`,`fio`.`Patronimyc` AS `CPatronimyc`,`clients`.`id` AS `cl_id`,`clients`.`Comment` AS `Comment`,`clients`.`TelNumber` AS `TelNumber`,`clients`.`inRedList` AS `inRedList` from (`fio` join `clients`) where (`fio`.`id` = `clients`.`id_FIO`) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `reception_view`
--

/*!50001 DROP TABLE IF EXISTS `reception_view`*/;
/*!50001 DROP VIEW IF EXISTS `reception_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`kvartetAdmin`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `reception_view` AS select `cls`.`CName` AS `CName`,`cls`.`CSurname` AS `CSurname`,`cls`.`CPatronimyc` AS `CPatronimyc`,`cls`.`cl_id` AS `cl_id`,`cls`.`Comment` AS `Comment`,`cls`.`TelNumber` AS `TelNumber`,`cls`.`inRedList` AS `inRedList`,`sps`.`SName` AS `SName`,`sps`.`SSurname` AS `SSurname`,`sps`.`SPatronimyc` AS `SPatronimyc`,`sps`.`spec_id` AS `spec_id`,`specializations`.`Specialization` AS `Specialization`,`cabinets`.`Name` AS `CabName`,`r`.`startTime` AS `startTime`,`r`.`endTime` AS `endTime`,`r`.`receptionDate` AS `receptionDate` from ((((`clients_view` `cls` join `specs_view` `sps`) join `receptioncards` `r`) join `specializations`) join `cabinets`) where ((`cls`.`cl_id` = `r`.`Client`) and (`sps`.`spec_id` = `r`.`Specialist`) and (`specializations`.`id` = `r`.`Specialization`) and (`cabinets`.`id` = `r`.`Cabinet`)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `specs_view`
--

/*!50001 DROP TABLE IF EXISTS `specs_view`*/;
/*!50001 DROP VIEW IF EXISTS `specs_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`kvartetAdmin`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `specs_view` AS select `fio`.`Name` AS `SName`,`fio`.`Surname` AS `SSurname`,`fio`.`Patronimyc` AS `SPatronimyc`,`specialist`.`id` AS `spec_id` from (`fio` join `specialist`) where (`fio`.`id` = `specialist`.`id_FIO`) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-10-09 17:34:46
