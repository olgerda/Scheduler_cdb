CREATE TABLE `clientGenerallyParams` (
  `idclientGenerallyParams` INT NOT NULL AUTO_INCREMENT,
  `clientId` INT ZEROFILL NOT NULL,
  `generallyTime` TIME NOT NULL,
  `generallyPrice` INT ZEROFILL NOT NULL,
  PRIMARY KEY (`idclientGenerallyParams`),
  UNIQUE INDEX `idclientGenerallyParams_UNIQUE` (`idclientGenerallyParams` ASC),
  UNIQUE INDEX `clientId_UNIQUE` (`clientId` ASC));