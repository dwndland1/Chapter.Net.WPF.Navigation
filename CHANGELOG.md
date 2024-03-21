# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.1.0] - 2024-03-21
### Added
- Added IActivator that provides an async activate method for a ViewModel which can get called when switch between ViewModels without disposing them before. For example custom tab controls.
### Fixed
- Made the SingleNavigationPresenter non focusable to be sure it does not get bad keyboard focus.
- Made the StackedNavigationPresenter non focusable to be sure it does not get bad keyboard focus.
### Supported .Net Versions
- .Net 6
- .Net 7
- .Net 8

## [1.0.0] - 2023-12-25
### Added
- Init project
### Supported .Net Versions
- .Net 6
- .Net 7
- .Net 8
