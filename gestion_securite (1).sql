-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : lun. 02 juin 2025 à 15:59
-- Version du serveur :  5.7.31
-- Version de PHP : 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `gestion_securite`
--

-- --------------------------------------------------------

--
-- Structure de la table `classe`
--

DROP TABLE IF EXISTS `classe`;
CREATE TABLE IF NOT EXISTS `classe` (
  `Code_classe` varchar(5) NOT NULL,
  `Libelle_classe` varchar(25) NOT NULL,
  PRIMARY KEY (`Code_classe`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `classe`
--

INSERT INTO `classe` (`Code_classe`, `Libelle_classe`) VALUES
('1', 'Classe 1');

-- --------------------------------------------------------

--
-- Structure de la table `entreprise`
--

DROP TABLE IF EXISTS `entreprise`;
CREATE TABLE IF NOT EXISTS `entreprise` (
  `id_entreprise` int(11) NOT NULL AUTO_INCREMENT,
  `nom_entreprise` varchar(100) NOT NULL,
  `adresse` varchar(255) NOT NULL,
  `code_postal` varchar(10) NOT NULL,
  `ville` varchar(50) NOT NULL,
  `telephone` varchar(15) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_entreprise`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `historiquemdp`
--

DROP TABLE IF EXISTS `historiquemdp`;
CREATE TABLE IF NOT EXISTS `historiquemdp` (
  `id_historique` int(11) NOT NULL AUTO_INCREMENT,
  `id_utilisateur` int(11) NOT NULL,
  `ancien_mot_de_passe` char(64) NOT NULL,
  `date_changement` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_historique`),
  KEY `id_utilisateur` (`id_utilisateur`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `professeur_classe`
--

DROP TABLE IF EXISTS `professeur_classe`;
CREATE TABLE IF NOT EXISTS `professeur_classe` (
  `id_professeur` int(11) NOT NULL,
  `Code_classe` varchar(5) NOT NULL,
  PRIMARY KEY (`id_professeur`,`Code_classe`),
  KEY `Code_classe` (`Code_classe`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `session`
--

DROP TABLE IF EXISTS `session`;
CREATE TABLE IF NOT EXISTS `session` (
  `id_session` int(11) NOT NULL AUTO_INCREMENT,
  `id_utilisateur` int(11) NOT NULL,
  `date_connexion` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `date_deconnexion` timestamp NULL DEFAULT NULL,
  `adresse_ip` varchar(45) DEFAULT NULL,
  `user_agent` varchar(255) DEFAULT NULL,
  `is_expired` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id_session`),
  KEY `id_utilisateur` (`id_utilisateur`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `stage`
--

DROP TABLE IF EXISTS `stage`;
CREATE TABLE IF NOT EXISTS `stage` (
  `id_stage` int(11) NOT NULL AUTO_INCREMENT,
  `id_eleve` int(11) NOT NULL,
  `id_entreprise` int(11) NOT NULL,
  `id_tuteur` int(11) NOT NULL,
  `date_debut` date NOT NULL,
  `date_fin` date NOT NULL,
  `sujet_stage` varchar(255) DEFAULT NULL,
  `rapport_stage` text,
  `evaluation_stage` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_stage`),
  KEY `id_eleve` (`id_eleve`),
  KEY `id_entreprise` (`id_entreprise`),
  KEY `id_tuteur` (`id_tuteur`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `suivi`
--

DROP TABLE IF EXISTS `suivi`;
CREATE TABLE IF NOT EXISTS `suivi` (
  `id_suivi` int(11) NOT NULL AUTO_INCREMENT,
  `id_professeur` int(11) NOT NULL,
  `id_eleve` int(11) NOT NULL,
  `id_stage` int(11) DEFAULT NULL,
  `date_visite` date DEFAULT NULL,
  `compte_rendu_visite` text,
  PRIMARY KEY (`id_suivi`),
  KEY `id_professeur` (`id_professeur`),
  KEY `id_eleve` (`id_eleve`),
  KEY `id_stage` (`id_stage`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `tuteur_entreprise`
--

DROP TABLE IF EXISTS `tuteur_entreprise`;
CREATE TABLE IF NOT EXISTS `tuteur_entreprise` (
  `id_tuteur` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `prenom` varchar(50) NOT NULL,
  `telephone` varchar(15) DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `id_entreprise` int(11) NOT NULL,
  PRIMARY KEY (`id_tuteur`),
  UNIQUE KEY `email` (`email`),
  KEY `id_entreprise` (`id_entreprise`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

DROP TABLE IF EXISTS `utilisateur`;
CREATE TABLE IF NOT EXISTS `utilisateur` (
  `id_utilisateur` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) NOT NULL,
  `prenom` varchar(50) DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `mot_de_passe` char(64) NOT NULL,
  `sel` varchar(64) NOT NULL,
  `role` enum('eleve','professeur','administrateur') NOT NULL,
  `date_naissance` date DEFAULT NULL,
  `rue` varchar(100) DEFAULT NULL,
  `code_postal` varchar(10) DEFAULT NULL,
  `ville` varchar(50) DEFAULT NULL,
  `Code_classe` varchar(5) DEFAULT NULL,
  `date_creation_compte` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `dernier_changement_mdp` date DEFAULT NULL,
  `expiration_mot_de_passe` date DEFAULT NULL,
  `reset_token` varchar(255) DEFAULT NULL,
  `reset_token_expiration` datetime DEFAULT NULL,
  `double_auth_code` varchar(10) DEFAULT NULL,
  `double_auth_expiration` datetime DEFAULT NULL,
  `double_auth_active` tinyint(1) DEFAULT '0',
  `is_active` tinyint(1) DEFAULT '0',
  `photo_path` varchar(2555) DEFAULT NULL,
  PRIMARY KEY (`id_utilisateur`),
  UNIQUE KEY `email` (`email`),
  KEY `Code_classe` (`Code_classe`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `utilisateur`
--

INSERT INTO `utilisateur` (`id_utilisateur`, `nom`, `prenom`, `email`, `mot_de_passe`, `sel`, `role`, `date_naissance`, `rue`, `code_postal`, `ville`, `Code_classe`, `date_creation_compte`, `dernier_changement_mdp`, `expiration_mot_de_passe`, `reset_token`, `reset_token_expiration`, `double_auth_code`, `double_auth_expiration`, `double_auth_active`, `is_active`, `photo_path`) VALUES
(1, 'sara', 'semedo', 'sarasemedo@mail.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', '', 'eleve', '2017-03-21', 'jhvgughjhgtcyffguyg', '13012', 'marseille', '1', '2025-03-13 13:40:05', '2025-03-13', '2025-03-30', NULL, NULL, NULL, NULL, NULL, NULL, '\"D:\\bts sio\\2TSSIO\\projetSecuriteFinale\\img\\OIP.jpg\"'),
(2, 'sayf', 'idoudi', 'sayfidoudi@gmail.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', '', 'professeur', '2025-03-11', 'cbqjbdcbqhcbdd', '13012', 'marseille', NULL, '2025-03-13 15:25:01', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(3, 'Michaud', 'Christian', 'michaudchris@gmail.com', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', '', 'administrateur', '1980-03-21', '3 rue jeanne d\'arc', '13005', 'Aubagne', '1', '2025-03-18 12:59:18', '2025-03-18', '2025-11-19', NULL, NULL, NULL, NULL, 0, 0, NULL),
(4, 'BEN YOUNES', 'Zakariya', 'jefaitduvevo@gmail.com', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', '', 'eleve', '2005-05-08', '47 boulevard Jeane d\'arc ', '13005', 'Marseille', '1', '2025-05-07 08:31:53', '2024-10-06', '2025-08-26', '9281f0e7-0a71-4148-95bb-a163d167f61a', '2025-05-28 19:42:45', NULL, NULL, 0, 0, 'https://thumbs.dreamstime.com/b/young-schoolboy-sitting-behind-school-desk-11436420.jpg'),
(5, 'BOUKAZOULA', 'Aymen', 'Aymen@mail.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', '', 'eleve', '2005-05-07', '14 rue polard', '13007', 'Marseille', '1', '2025-05-07 08:31:53', '2023-12-04', '2025-05-05', NULL, NULL, NULL, NULL, 0, 0, '\"D:\\bts sio\\2TSSIO\\projetSecuriteFinale\\img\\young-schoolboy-sitting-behind-school-desk-11436420.webp\"'),
(6, 'DECOMIS', 'Gabriel', 'Gabriel@mail.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', '', 'eleve', '2005-05-06', '12 bouelbard hump', '13001', 'Marseille', '1', '2025-05-07 08:38:19', '2025-05-06', '2025-12-24', NULL, NULL, NULL, NULL, 0, 0, NULL),
(7, 'Kolina', 'Taehyung', 'kooktaeka@gmail.com', 'v913nzVo3zQkujniMDS6cC2xA+Be3CRYp8YFZKT1ZRc=', 'XMmy1M6vVy45HRKT1bdZYA==', 'eleve', '2004-12-17', 'la vuie du ru 78', '12554', 'montpellier', '1', '2025-05-28 09:06:05', '2025-06-02', '2025-08-31', NULL, NULL, NULL, NULL, 0, 0, 'https://aura.apprentis-auteuil.org/files/2018/08/715061819.jpg'),
(8, 'bugnoli', 'Alexandre', 'alexandrebugnoli@gmail.com', 'd83a0ff560d6bb2936e3a5e84c3998c6e31fc71129cb40f44d1618378581eac7', '1oDR8cRe3bQ81/zIanGTdg==', 'eleve', '2005-09-01', NULL, NULL, NULL, NULL, '2025-05-28 16:46:03', '2025-05-28', '2025-08-26', NULL, NULL, NULL, NULL, 0, 0, NULL),
(9, '', NULL, '', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', '', 'eleve', '2005-09-01', NULL, NULL, NULL, NULL, '2025-05-28 16:46:03', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `historiquemdp`
--
ALTER TABLE `historiquemdp`
  ADD CONSTRAINT `historiquemdp_ibfk_1` FOREIGN KEY (`id_utilisateur`) REFERENCES `utilisateur` (`id_utilisateur`);

--
-- Contraintes pour la table `professeur_classe`
--
ALTER TABLE `professeur_classe`
  ADD CONSTRAINT `professeur_classe_ibfk_1` FOREIGN KEY (`id_professeur`) REFERENCES `utilisateur` (`id_utilisateur`),
  ADD CONSTRAINT `professeur_classe_ibfk_2` FOREIGN KEY (`Code_classe`) REFERENCES `classe` (`Code_classe`);

--
-- Contraintes pour la table `session`
--
ALTER TABLE `session`
  ADD CONSTRAINT `session_ibfk_1` FOREIGN KEY (`id_utilisateur`) REFERENCES `utilisateur` (`id_utilisateur`);

--
-- Contraintes pour la table `stage`
--
ALTER TABLE `stage`
  ADD CONSTRAINT `stage_ibfk_1` FOREIGN KEY (`id_eleve`) REFERENCES `utilisateur` (`id_utilisateur`),
  ADD CONSTRAINT `stage_ibfk_2` FOREIGN KEY (`id_entreprise`) REFERENCES `entreprise` (`id_entreprise`),
  ADD CONSTRAINT `stage_ibfk_3` FOREIGN KEY (`id_tuteur`) REFERENCES `tuteur_entreprise` (`id_tuteur`);

--
-- Contraintes pour la table `suivi`
--
ALTER TABLE `suivi`
  ADD CONSTRAINT `suivi_ibfk_1` FOREIGN KEY (`id_professeur`) REFERENCES `utilisateur` (`id_utilisateur`),
  ADD CONSTRAINT `suivi_ibfk_2` FOREIGN KEY (`id_eleve`) REFERENCES `utilisateur` (`id_utilisateur`),
  ADD CONSTRAINT `suivi_ibfk_3` FOREIGN KEY (`id_stage`) REFERENCES `stage` (`id_stage`);

--
-- Contraintes pour la table `tuteur_entreprise`
--
ALTER TABLE `tuteur_entreprise`
  ADD CONSTRAINT `tuteur_entreprise_ibfk_1` FOREIGN KEY (`id_entreprise`) REFERENCES `entreprise` (`id_entreprise`);

--
-- Contraintes pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  ADD CONSTRAINT `utilisateur_ibfk_1` FOREIGN KEY (`Code_classe`) REFERENCES `classe` (`Code_classe`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
