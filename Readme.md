# GRM Task

## Overview

This is a .NET console application that filters active music contracts for a given partner and effective date.

The application processes two input data files:

- musicContracts.txt — music contract definitions
- partnerContracts.txt — partner usage periods

Both files are included in the repository.

## Prerequisites

- .NET SDK 8.0 installed
- Command line / terminal access

Check installation:

```bash
dotnet --version
```

## Build

To build the project:

```bash
dotnet build
```

## Run Application

The application requires two arguments:

```
GrmTask <PartnerName> <EffectiveDate>
```

Example:

```bash
dotnet run --project GrmTask.csproj YouTube "27th Dec 2012"
```

Another example:

```bash
dotnet run --project GrmTask.csproj ITunes "1st March 2012"
```

## Invalid Usage

If arguments are missing or incorrect, the application will display:

```
Usage: GrmTask <PartnerName> <EffectiveDate>
```

Make sure:

- Partner name is provided
- Date is provided
- Date format is correct

## Run Tests

To execute unit tests:

```bash
dotnet test
```
