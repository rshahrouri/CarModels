# Car Models API

This project implements a REST API for retrieving car models based on the make and manufacture year. It utilizes a third-party API for fetching model data.

## Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository:

```bash
git clone https://github.com/rshahrouri/car-models-api.git
```

2. Navigate to the project directory:

```bash
cd car-models-api
```

3. Build and run the application:
```bash
dotnet build
dotnet run
```

The application will be accessible at `http://localhost:7117`.

## API Endpoints

Get Models for a Specific Car:
```bash
GET /api/models?modelyear={year}&make={make}
```

Example:
```bash
curl http://localhost:7117/api/models?modelyear=2015&make=Lincoln
```

Response:
```bash
{
  "Models": ["Town Car", "Continental", "Mark"]
}
```

## Project Structure

The project follows the following structure:

**Controllers**: Contains the API controllers.

**DTOs**: Data Transfer Objects for API responses.

**Middlewares**: Custom middleware for handling errors.

**Services**: Business logic services.

**Extensions**: Extension methods for configuring services.

