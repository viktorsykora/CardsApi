# CardsApi

## How to run
docker build -t cards-api .<br>
docker run -d -p 8080:8080 -p 8081:8081 --name cards-api-container cards-api

## Sample request
curl -X GET http://localhost:8080/api/cards/123456789/validity \
     -H "x-api-key: 0de0cd7e-573f-446d-83ab-740ed6076200"

## OpenAPI endpoint
http://localhost:8080/openapi/v1.json

## Health endpoint
http://localhost:8080/healthz

## Important remarks
- When built and run with docker the API will use HTTP. This is unsafe in combination with x-api-key authentication. For HTTPS enabled the certificate should be mounted using docker compose.<br>
- API key is stored in settings for simplicity. Production settings should use for example appsettings + secrets injected from secrets manager like Azure KeyVault.
