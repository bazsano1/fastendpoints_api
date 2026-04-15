# FastEndpoints API

A modern REST API built with **FastEndpoints** framework running on **.NET 10**.

## 📋 Table of Contents

- [Overview](#overview)
- [Repository Structure](#repository-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Testing](#testing)
- [Project Structure](#project-structure)

## Overview

This project is a lightweight REST API demonstrating best practices with the FastEndpoints framework. It provides endpoints for managing songs with minimal overhead and maximum performance.

**Key Features:**
- Built with FastEndpoints framework
- .NET 10 target framework
- Simple, clean endpoint architecture
- Type-safe request/response handling

## Repository Structure

```
fastendpoints_api/
├── src/
│   └── FastEndpointApi/
│       ├── Domain/                 # Data models and DTOs
│       │   ├── SongRequest.cs      # Request model for songs
│       │   └── SongResponse.cs     # Response model for songs
│       ├── Endpoints/              # FastEndpoints handlers
│       │   ├── HelloWorld.cs       # Hello World endpoint
│       │   └── SongEndpoint.cs     # Song management endpoint
│       ├── Program.cs              # Application configuration
│       ├── appsettings.json        # Configuration settings
│       ├── appsettings.Development.json
│       └── FastEndpointApi.csproj  # Project file
├── .gitignore                      # Git ignore rules
└── README.md                       # This file
```

## Prerequisites

- **.NET 10 SDK** or later
- **Visual Studio 2026** (or Visual Studio Code)
- **PowerShell** or your preferred terminal

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/bazsano1/fastendpoints_api.git
cd fastendpoints_api
```

### 2. Navigate to Project Directory

```bash
cd src/FastEndpointApi
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Build the Project

```bash
dotnet build
```

### 5. Run the Application

```bash
dotnet run
```

The API will start on `https://localhost:5000` (or the configured port in `launchSettings.json`).

## API Documentation

### Base URL

```
https://localhost:5000
```

### Endpoints

#### 1. **Hello World Endpoint**

Returns a simple greeting message.

- **URL:** `/`
- **Method:** `GET`
- **Authentication:** Not required
- **Description:** Returns a "Hello World" message from FastEndpoint

**Example Request:**

```bash
curl -X GET https://localhost:5000/
```

**Example Response:**

```
"Hello World from FastEndpoint!"
```

---

#### 2. **Song Endpoint**

Create and retrieve song information.

- **URL:** `/api/songs`
- **Method:** `POST`
- **Authentication:** Not required
- **Description:** Accepts song data and returns the created song

**Request Body (JSON):**

```json
{
  "author": "string",
  "title": "string",
  "genre": "string",
  "year": integer
}
```

**Request Parameters:**

| Parameter | Type    | Required | Description        |
|-----------|---------|----------|--------------------|
| author    | string  | No       | Artist/Author name |
| title     | string  | No       | Song title         |
| genre     | string  | No       | Music genre        |
| year      | integer | No       | Release year       |

**Example Request:**

```bash
curl -X POST https://localhost:5000/api/songs \
  -H "Content-Type: application/json" \
  -d '{
	"author": "6363",
	"title": "etiket",
	"genre": "hiphop",
	"year": 2026
  }'
```

**Example Response (200 OK):**

```json
{
  "author": "6363",
  "title": "etiket",
  "genre": "hiphop",
  "year": 2026
}
```

---

## Testing

### Using cURL

#### Test 1: Hello World Endpoint

```bash
# Test the GET endpoint
curl -X GET https://localhost:5000/ -v
```

**Expected Output:**
```
HTTP/1.1 200 OK
"Hello World from FastEndpoint!"
```

---

#### Test 2: Create Song (Valid Request)

```bash
# Create a new song
curl -X POST https://localhost:5000/api/songs \
  -H "Content-Type: application/json" \
  -d '{
	"author": "The Beatles",
	"title": "Let It Be",
	"genre": "Rock",
	"year": 1970
  }' \
  -v
```

**Expected Output:**
```json
{
  "author": "The Beatles",
  "title": "Let It Be",
  "genre": "Rock",
  "year": 1970
}
```

---

#### Test 3: Create Song (Minimal Data)

```bash
# Create a song with minimal data
curl -X POST https://localhost:5000/api/songs \
  -H "Content-Type: application/json" \
  -d '{
	"author": "Unknown Artist"
  }' \
  -v
```

**Expected Output:**
```json
{
  "author": "Unknown Artist",
  "title": null,
  "genre": null,
  "year": null
}
```

---

#### Test 4: Create Song (All Fields)

```bash
# Create a song with all fields
curl -X POST https://localhost:5000/api/songs \
  -H "Content-Type: application/json" \
  -d '{
	"author": "Drake",
	"title": "One Dance",
	"genre": "Hip-Hop",
	"year": 2016
  }' \
  -v
```

---

### Using PowerShell

```powershell
# Test Hello World endpoint
$response = Invoke-WebRequest -Uri "https://localhost:5000/" -Method Get
$response.Content

# Test Song endpoint
$body = @{
	author = "6363"
	title = "etiket"
	genre = "hiphop"
	year = 2026
} | ConvertTo-Json

$response = Invoke-WebRequest -Uri "https://localhost:5000/api/songs" `
	-Method Post `
	-ContentType "application/json" `
	-Body $body

$response.Content | ConvertFrom-Json | ConvertTo-Json
```

---

### Using Postman

1. **Import the API:**
   - Create a new request collection
   - Add the following requests

2. **GET Hello World:**
   - Method: `GET`
   - URL: `https://localhost:5000/`
   - Click "Send"

3. **POST Create Song:**
   - Method: `POST`
   - URL: `https://localhost:5000/api/songs`
   - Headers: `Content-Type: application/json`
   - Body (raw JSON):
   ```json
   {
	 "author": "6363",
	 "title": "etiket",
	 "genre": "hiphop",
	 "year": 2026
   }
   ```
   - Click "Send"

---

## Project Structure

### Domain/

Contains data models used for request/response handling:

- **SongRequest.cs:** Defines the input model for song creation
  ```csharp
  public class SongRequest
  {
	  public string? Author { get; set; }
	  public string? Title { get; set; }
	  public string? Genre { get; set; }
	  public int? Year { get; set; }
  }
  ```

- **SongResponse.cs:** Defines the output model for song responses
  ```csharp
  public class SongResponse
  {
	  public required string Author { get; set; }
	  public required string Title { get; set; }
	  public required string Genre { get; set; }
	  public int Year { get; set; }
  }
  ```

### Endpoints/

Contains the FastEndpoints route handlers:

- **HelloWorld.cs:** GET endpoint at `/` returning a greeting
- **SongEndpoint.cs:** POST endpoint at `/api/songs` for song management

### Configuration

- **Program.cs:** Sets up FastEndpoints and application services
- **appsettings.json:** Production configuration
- **appsettings.Development.json:** Development-specific settings

---

## Development Workflow

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run
```

### Watch Mode (Auto-reload)

```bash
dotnet watch run
```

### Format Code

```bash
dotnet format
```

---

## Contributing

1. Create a feature branch
2. Make your changes
3. Test thoroughly
4. Submit a pull request

---

## License

This project is provided as-is for educational and development purposes.

---

## Support

For issues or questions, please open an issue on GitHub or contact the repository maintainers.

---

**Last Updated:** 2024
**Target Framework:** .NET 10
**Framework:** FastEndpoints
