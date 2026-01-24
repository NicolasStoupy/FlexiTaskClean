/*
 * GlobalUsings.cs
 *
 * Centralise les directives `global using` pour la couche Application.
 * Objectifs :
 * - Réduire le bruit dans les fichiers source en exposant certains espaces de noms
 *   à l'ensemble du projet Application.
 * - Regrouper les dépendances communes (interfaces de l'application, constantes et enums
 *   du domaine, bibliothèques tierces utilisées par l'Application).
 *
 * Bonnes pratiques :
 * - Ne placer ici que les `global using` réellement partagés par plusieurs fichiers
 *   dans la couche Application afin d'éviter l'ajout de dépendances inutiles.
 * - Conserver des sections claires (Application, Domain, Assemblies) pour la lisibilité.
 */

//Application
global using Application.Common.Interfaces;
global using Domain.Constants;
// Domain
global using Domain.Enums;

//Assemblies
global using Microsoft.EntityFrameworkCore;
global using AutoMapper;
global using AutoMapper.QueryableExtensions;
global using MediatR;
global using FluentValidation;