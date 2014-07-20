CREATE TABLE `specialist2clientprice` (
  `idspecialist2clientcost` int(11) NOT NULL AUTO_INCREMENT,
  `specid` int(11) NOT NULL,
  `clid` int(11) NOT NULL,
  `price` int(10) unsigned zerofill NOT NULL DEFAULT '0000000000',
  PRIMARY KEY (`idspecialist2clientcost`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
ALTER TABLE `clients` DROP COLUMN `price`;