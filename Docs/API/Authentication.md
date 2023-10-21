# Authentication

## Login

**POST**: /api/auth/login
**Body:**

```json
{
  "email": "string",
  "password": "string"
}
```

**Result:**

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "emailConfirmed": "boolean",
  "phone": "string | undefined",
  "phoneConfirmed": "boolean",
  "token": "string",
  "refreshToken": "string"
}
```

## Register

**POST**: /api/auth/register
**Body:**

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "password": "string"
}
```

**Result:**

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "emailConfirmed": "boolean",
  "phone": "string | undefined",
  "phoneConfirmed": "boolean",
  "token": "string",
  "refreshToken": "string"
}
```

## Refresh

**POST**: /api/auth/refresh
**Body:**

```json
{
  "token": "string"
}
```

**Result:**

```json
{
  "token": "string",
  "refreshToken": "string"
}
```

## Errors

Any errors will result:

```json
{
  "type": "string",
  "title": "string",
  "status": "int",
  "traceId": "string",
  "errorCodes": "string[]"
}
```
