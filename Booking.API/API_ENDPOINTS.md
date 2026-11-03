# Booking API — pregled endpoint-a za testiranje

Sve rute su relativne na `/api`. Za rute koje traže autorizaciju, dodaj `Authorization: Bearer {token}` header (token iz `/api/auth` login-a).

## Auth

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/auth` | javno | `{ "email": "", "password": "" }` |
| POST | `/auth/logout` | ulogovan (Bearer header) | - |
| POST | `/auth/refresh` | javno | `{ "refreshToken": "" }` |

## Register

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/register` | javno | `{ "firstName": "", "lastName": "", "email": "", "password": "", "roleId": 0 }` |

## Property

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/property` | Admin, Landlord | multipart/form-data: `Name, Description, Address, PropertyTypeId, DestinationId, LandlordId (Admin only), Image` |
| GET | `/property` | javno | query: `Name, City, Country, MinCapacity, MinPrice, MaxPrice, Page, PageSize` |
| GET | `/property/{id}` | javno | - |
| **PUT** | `/property/{id}` | **NOVO** — Admin, vlasnik Landlord | `{ "name": "", "description": "", "address": "", "propertyTypeId": 0, "destinationId": 0 }` |
| **DELETE** | `/property/{id}` | **NOVO** — Admin, vlasnik Landlord | blokirano ako property ima rezervacije |

## PropertyRoom

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/propertyroom` | Admin, Landlord | multipart/form-data: `Name, Capacity, PropertyId, Image` |
| POST | `/propertyroom/attach` | Admin, Landlord | multipart/form-data: `PropertyRoomId, Images[]` |
| GET | `/propertyroom/{id}` | javno | - |
| **GET** | `/propertyroom/{id}/availability` | **NOVO** — javno | query: `startDate, endDate` |
| **PUT** | `/propertyroom/{id}` | **NOVO** — Admin, vlasnik Landlord | `{ "name": "", "capacity": 0 }` |
| **DELETE** | `/propertyroom/{id}` | **NOVO** — Admin, vlasnik Landlord | blokirano ako soba ima rezervacije |

## Attachment (slike)

| Method | Route | Rola | Body |
|---|---|---|---|
| **DELETE** | `/attachment/{id}` | **NOVO** — Admin, vlasnik Landlord | blokirano ako je poslednja slika property-ja/sobe |

## Price (PropertyRoomPrice)

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/price` | Admin, Landlord | `{ "propertyRoomId": 0, "startDate": "", "endDate": null, "price": 0 }` |
| **PUT** | `/price/{id}` | **NOVO** — Admin, vlasnik Landlord | `{ "date": "", "price": 0 }` — blokirano ako je dan već rezervisan |
| **DELETE** | `/price/{id}` | **NOVO** — Admin, vlasnik Landlord | blokirano ako je dan već rezervisan |

## Amenity

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/amenity` | Admin | `{ "name": "" }` |
| POST | `/amenity/attach` | Landlord, Admin | `{ "amenityId": 0, "propertyId": 0 }` |
| GET | `/amenity` | javno | - |
| **DELETE** | `/amenity/detach` | **NOVO** — Landlord, Admin | query: `propertyId, amenityId` |

## Reservation

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/reservation` | Tenant, Landlord | `{ "propertyRoomId": 0, "startDate": "", "endDate": "" }` |
| GET | `/reservation` | ulogovan | query: `Page, PageSize` (rezervacije trenutnog korisnika) |
| GET | `/reservation/landlord` | Landlord | rezervacije za property-je koje poseduje |
| POST | `/reservation/confirm` | Landlord | `{ "reservationId": 0 }` |
| POST | `/reservation/cancel` | Tenant, Landlord | `{ "reservationId": 0 }` |

## Review

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/review` | Tenant | `{ "propertyId": 0, "rating": 0, "comment": "" }` — sad zahteva da je boravak (rezervacija) završen |
| **PUT** | `/review/{id}` | **NOVO** — autor ili Admin | `{ "rating": 0, "comment": "" }` |
| **DELETE** | `/review/{id}` | **NOVO** — autor ili Admin | - |
| GET | `/review/property/{propertyId}` | javno | query: `page, pageSize` |

## User

| Method | Route | Rola | Body |
|---|---|---|---|
| GET | `/user/profile` | ulogovan | rezervacije/ocene (Tenant) ili posedovani property-ji (Landlord) |
| **GET** | `/user` | **NOVO** — Admin | query: `Email, RoleName, IsActive, Page, PageSize` |
| **PUT** | `/user/{id}/role` | **NOVO** — Admin | `{ "roleId": 0 }` |
| **PUT** | `/user/{id}/status` | **NOVO** — Admin | `{ "isActive": true/false }` |
| **GET** | `/user/landlord/stats` | **NOVO** — Landlord | broj property-ja/soba, rezervacije po statusu, prihod, prosečna ocena |
| **PUT** | `/user/password` | **NOVO** — ulogovan | `{ "oldPassword": "", "newPassword": "" }` |

## AuditLog

| Method | Route | Rola | Body |
|---|---|---|---|
| **GET** | `/auditlog` | **NOVO** — Admin | query: `UserEmail, CommandName, Success, From, To, Page, PageSize` |

## UseCase (autorizacija po use case-u)

| Method | Route | Rola | Body |
|---|---|---|---|
| POST | `/usecase` | Admin | `{ "name": "", "useCaseId": "" }` (npr. `useCaseId: "ef-add-property"`) |
| POST | `/usecase/attach` | Admin | `{ "roleId": 0, "useCaseId": 0 }` (useCaseId je ovde int PK) |
| GET | `/usecase` | Admin | - |
| **DELETE** | `/usecase/detach` | **NOVO** — Admin | query: `roleId, useCaseId` (int PK) |

## Reference podaci (Country / Destination / PropertyType / Role)

| Method | Route | Rola |
|---|---|---|
| POST / GET | `/country` | POST: Admin, GET: javno |
| POST / GET | `/destination` | POST: Admin, GET: javno |
| GET | `/propertytype` | javno |
| POST / GET | `/role` | Admin |

---

### Napomene za testiranje
- Za sve `PUT`/`DELETE` rute iznad koje zahtevaju "vlasnik Landlord ili Admin" — uloguj se kao Landlord koji **stvarno poseduje** taj property da bi prošla provera, ili kao Admin da zaobiđeš proveru vlasništva.
- `PUT /property/{id}`, `PUT /propertyroom/{id}` itd. vraćaju `204 No Content` na uspeh.
- Pre testiranja `PUT/DELETE /user/{id}/...` i `GET /user`, pokreni migraciju za novo `IsActive` polje na `User` (`dotnet ef migrations add UserIsActive ...`), inače će pucati na nepostojećoj koloni.
- `GET /auditlog` će odmah imati podatke jer se svaki poziv bilo koje komande loguje automatski.
