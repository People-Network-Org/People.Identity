# Authentication

## Register

**POST**: /api/auth/register

**Body**:

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

**Result**:

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "phone": "string | undefined"
}
```

## Confirm by code

**POST** /api/auth/confirm

**Body**:

```json
{
  "emailCode": "string",
  "password": "string"
}
```

**Result**:

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "phone": "string | undefined",
  "token": "string",
  "refreshToken": "string"
}
```

## Get user by confirmation code

**GET**: /api/auth/confirm/{code:string}

**Result**:

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "phone": "string | undefined"
}
```

## Login

**POST**: /api/auth/login

**Body**:

```json
{
  "email": "string",
  "password": "string"
}
```

**Result**:

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "phone": "string | undefined",
  "token": "string",
  "refreshToken": "string"
}
```

## Refresh

**POST**: /api/auth/refresh

**Body**:

```json
{
  "refreshToken": "string"
}
```

**Result**:

```json
{
  "token": "string",
  "refreshToken": "string"
}
```

## Logout

**POST**: /api/auth/logout

**Body**:

```json
{
  "refreshToken": "string"
}
```

## Me

**GET**: /api/auth/me

**Result**:

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "phone": "string | undefined"
}
```

## Change password

**PUT**:/api/auth/password

**Body**:

```json
{
  "password": "string"
}
```

**Result**:

```json
{
  "id": "string",
  "firstName": "string",
  "lastName": "string | undefined",
  "nickName": "string",
  "email": "string",
  "phone": "string | undefined",
  "token": "string",
  "refreshToken": "string"
}
```
