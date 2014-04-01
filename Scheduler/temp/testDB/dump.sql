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
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cabinets`
--

LOCK TABLES `cabinets` WRITE;
/*!40000 ALTER TABLE `cabinets` DISABLE KEYS */;
INSERT INTO `cabinets` VALUES (1,'ПервыйКабинет'),(2,'ВторойКабинет'),(3,'ТретийКабинет'),(4,'ЧетвёртыйКабинет'),(5,'111'),(6,'123'),(7,'333');
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
  `Name` varchar(200) COLLATE utf8_bin NOT NULL,
  `Comment` text COLLATE utf8_bin NOT NULL,
  `inRedList` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `id_FIO` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clients`
--

LOCK TABLES `clients` WRITE;
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients` VALUES (1,'7','первый комментарий',0),(2,'8','второй комментарий',0),(3,'9','третий комментарий',0);
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
  `cl_id` tinyint NOT NULL,
  `Name` tinyint NOT NULL,
  `Comment` tinyint NOT NULL,
  `inRedList` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `phones`
--

DROP TABLE IF EXISTS `phones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `phones` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `client_id` smallint(5) unsigned NOT NULL,
  `phonescol` tinytext COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Telephone numbers as tinytext';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phones`
--

LOCK TABLES `phones` WRITE;
/*!40000 ALTER TABLE `phones` DISABLE KEYS */;
INSERT INTO `phones` VALUES (1,1,'9261234567'),(2,2,'9269876547'),(3,1,'9092929292'),(4,3,'1039475325');
/*!40000 ALTER TABLE `phones` ENABLE KEYS */;
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
  `cl_id` tinyint NOT NULL,
  `Comment` tinyint NOT NULL,
  `inRedList` tinyint NOT NULL,
  `SName` tinyint NOT NULL,
  `spec_id` tinyint NOT NULL,
  `Specialization` tinyint NOT NULL,
  `CabName` tinyint NOT NULL,
  `startTime` tinyint NOT NULL,
  `endTime` tinyint NOT NULL,
  `receptionDate` tinyint NOT NULL,
  `id` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `receptioncards`
--

DROP TABLE IF EXISTS `receptioncards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `receptioncards` (
  `id` bigint(20) unsigned NOT NULL,
  `Client` smallint(5) unsigned NOT NULL,
  `Specialist` smallint(5) unsigned NOT NULL,
  `Specialization` tinytext COLLATE utf8_bin NOT NULL,
  `Cabinet` smallint(5) unsigned NOT NULL,
  `startTime` time NOT NULL DEFAULT '10:00:00',
  `endTime` time NOT NULL DEFAULT '12:00:00',
  `receptionDate` date NOT NULL DEFAULT '2013-10-07',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `Client` (`Client`),
  KEY `Specialist` (`Specialist`),
  KEY `Cabinet` (`Cabinet`),
  CONSTRAINT `receptioncards_ibfk_1` FOREIGN KEY (`Client`) REFERENCES `clients` (`id`),
  CONSTRAINT `receptioncards_ibfk_2` FOREIGN KEY (`Specialist`) REFERENCES `specialist` (`id`),
  CONSTRAINT `receptioncards_ibfk_4` FOREIGN KEY (`Cabinet`) REFERENCES `cabinets` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receptioncards`
--

LOCK TABLES `receptioncards` WRITE;
/*!40000 ALTER TABLE `receptioncards` DISABLE KEYS */;
INSERT INTO `receptioncards` VALUES (1,1,1,'1',1,'10:00:00','12:00:00','2013-10-14'),(2,2,2,'2',2,'12:12:00','13:30:00','2013-10-14');
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
  `Name` varchar(200) COLLATE utf8_bin NOT NULL,
  `Comment` text COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id_FIO` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specialist`
--

LOCK TABLES `specialist` WRITE;
/*!40000 ALTER TABLE `specialist` DISABLE KEYS */;
INSERT INTO `specialist` VALUES (1,'Именитый имя Именович',''),(2,'Сохранивший Сохранит Сохранитович',''),(3,'3',''),(4,'4',''),(5,'5',''),(6,'6','');
/*!40000 ALTER TABLE `specialist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specializations`
--

DROP TABLE IF EXISTS `specializations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `specializations` (
  `id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `Specialization` mediumtext COLLATE utf8_bin NOT NULL,
  `SpecID` smallint(5) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specializations`
--

LOCK TABLES `specializations` WRITE;
/*!40000 ALTER TABLE `specializations` DISABLE KEYS */;
INSERT INTO `specializations` VALUES (1,'Специализация1',1),(2,'Специализация2',2),(3,'Хирург',3),(4,'Офтальмолог',4),(5,'Ортопед',3),(6,'Педиатр',2),(7,'ЛОР',3);
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
  `spec_id` tinyint NOT NULL,
  `Name` tinyint NOT NULL,
  `specialization` tinyint NOT NULL
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
/*!50001 VIEW `clients_view` AS select `clients`.`id` AS `cl_id`,`clients`.`Name` AS `Name`,`clients`.`Comment` AS `Comment`,`clients`.`inRedList` AS `inRedList` from `clients` */;
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
/*!50001 VIEW `reception_view` AS select `cls`.`Name` AS `CName`,`cls`.`cl_id` AS `cl_id`,`cls`.`Comment` AS `Comment`,`cls`.`inRedList` AS `inRedList`,`sps`.`Name` AS `SName`,`sps`.`spec_id` AS `spec_id`,`specializations`.`Specialization` AS `Specialization`,`cabinets`.`Name` AS `CabName`,`r`.`startTime` AS `startTime`,`r`.`endTime` AS `endTime`,`r`.`receptionDate` AS `receptionDate`,`r`.`id` AS `id` from ((((`clients_view` `cls` join `specs_view` `sps`) join `receptioncards` `r`) join `specializations`) join `cabinets`) where ((`cls`.`cl_id` = `r`.`Client`) and (`sps`.`spec_id` = `r`.`Specialist`) and (`specializations`.`id` = `r`.`Specialization`) and (`cabinets`.`id` = `r`.`Cabinet`)) */;
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
/*!50001 VIEW `specs_view` AS select `s`.`id` AS `spec_id`,`s`.`Name` AS `Name`,`specs`.`Specialization` AS `specialization` from (`specialist` `s` join `specializations` `specs`) where (`specs`.`SpecID` = `s`.`id`) */;
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

-- Dump completed on 2013-10-26 16:15:08
