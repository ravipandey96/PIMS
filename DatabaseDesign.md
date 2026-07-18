# Product Inventory Management System (PIMS)

# Database Design Document

Version: 1.0

Database: SQL Server

Approach: Entity Framework Core Code First

Author: Ravi Pandey

---

# 1. Database Overview

The Product Inventory Management System (PIMS) is designed to manage products, categories, inventory levels, inventory transactions, and users with role-based authorization.

The system follows relational database principles and is normalized to avoid data redundancy.

---

# 2. Database Tables

The system consists of the following tables.

1. Users

2. Roles

3. Products

4. Categories

5. ProductCategories

6. Inventory

7. InventoryTransactions

---

# 3. Table Design

--------------------------------------------
Table : Roles
--------------------------------------------

Description

Stores user roles.

Columns

RoleId
• Data Type : INT
• Primary Key
• Identity(1,1)
• Required

RoleName
• Data Type : NVARCHAR(50)
• Required
• Unique

Description
• Data Type : NVARCHAR(250)
• Nullable

CreatedDate
• DATETIME
• Required
• Default GETDATE()

Relationships

One Role
↓

Many Users

Indexes

UX_Roles_RoleName

--------------------------------------------
Table : Users
--------------------------------------------

Description

Stores application users.

Columns

UserId
• INT
• PK
• Identity

RoleId
• INT
• FK → Roles.RoleId

FirstName
• NVARCHAR(100)
• Required

LastName
• NVARCHAR(100)
• Required

Email
• NVARCHAR(200)
• Required
• Unique

PasswordHash
• NVARCHAR(MAX)
• Required

PhoneNumber
• NVARCHAR(20)
• Nullable

IsActive
• BIT
• Default True

CreatedDate
• DATETIME
• Default GETDATE()

ModifiedDate
• DATETIME
• Nullable

Relationships

One User belongs to One Role

Indexes

UX_Users_Email

IX_Users_RoleId

--------------------------------------------
Table : Categories
--------------------------------------------

Description

Stores product categories.

Columns

CategoryId
• INT
• PK

Name
• NVARCHAR(100)
• Required
• Unique

Description
• NVARCHAR(500)
• Nullable

IsActive
• BIT
• Default True

CreatedDate
• DATETIME

Indexes

UX_Categories_Name

--------------------------------------------
Table : Products
--------------------------------------------

Description

Stores all products.

Columns

ProductId
• INT
• PK

Name
• NVARCHAR(200)
• Required

Description
• NVARCHAR(1000)
• Nullable

SKU
• NVARCHAR(50)
• Required
• Unique

Price
• DECIMAL(18,2)
• Required

IsActive
• BIT
• Default True

CreatedDate
• DATETIME

ModifiedDate
• DATETIME
• Nullable

Indexes

UX_Products_SKU

IX_Products_Name

--------------------------------------------
Table : ProductCategories
--------------------------------------------

Description

Bridge table for many-to-many relationship.

Columns

ProductId
• INT
• FK

CategoryId
• INT
• FK

Primary Key

(ProductId, CategoryId)

Relationships

Many Products

↓

Many Categories

--------------------------------------------
Table : Inventory
--------------------------------------------

Description

Stores current inventory of products.

Columns

InventoryId
• INT
• PK

ProductId
• INT
• FK

Quantity
• INT
• Required

WarehouseLocation
• NVARCHAR(200)

LowStockThreshold
• INT

LastUpdated
• DATETIME

Indexes

IX_Inventory_ProductId

--------------------------------------------
Table : InventoryTransactions
--------------------------------------------

Description

Stores inventory movement history.

Columns

InventoryTransactionId
• INT
• PK

InventoryId
• INT
• FK

TransactionType
• NVARCHAR(50)

Quantity
• INT

Reason
• NVARCHAR(500)

PerformedByUserId
• INT
• FK

TransactionDate
• DATETIME

Indexes

IX_InventoryTransactions_InventoryId

IX_InventoryTransactions_Date

--------------------------------------------

# 4. Relationships

Roles (1)
↓

Users (Many)

Products (Many)
↓

ProductCategories

Categories (Many)

Products (1)
↓

Inventory (1)

Inventory (1)
↓

InventoryTransactions (Many)

Users (1)
↓

InventoryTransactions (Many)

---

# 5. Primary Keys

Roles
RoleId

Users
UserId

Products
ProductId

Categories
CategoryId

Inventory
InventoryId

InventoryTransactions
InventoryTransactionId

ProductCategories
(ProductId, CategoryId)

---

# 6. Foreign Keys

Users.RoleId

→ Roles.RoleId

ProductCategories.ProductId

→ Products.ProductId

ProductCategories.CategoryId

→ Categories.CategoryId

Inventory.ProductId

→ Products.ProductId

InventoryTransactions.InventoryId

→ Inventory.InventoryId

InventoryTransactions.PerformedByUserId

→ Users.UserId

---

# 7. Unique Constraints

RoleName

Email

SKU

Category Name

---

# 8. Default Values

CreatedDate

GETDATE()

IsActive

TRUE

---

# 9. Indexes

Products

SKU

Product Name

Users

Email

RoleId

Categories

Name

Inventory

ProductId

InventoryTransactions

InventoryId

TransactionDate

---

# 10. Business Rules

1. SKU must be unique.

2. Email must be unique.

3. Role name must be unique.

4. Category name must be unique.

5. Product price cannot be negative.

6. Inventory quantity cannot be negative.

7. Every inventory must belong to exactly one product.

8. A product can belong to multiple categories.

9. Inventory transactions are immutable (never updated after creation).

10. Passwords must never be stored as plain text.

11. Only Administrators can:
    - Adjust product prices
    - Perform inventory audits
    - Manage users

12. Users can:
    - View products
    - View inventory
    - Search products

---

# 11. Future Enhancements

Refresh Tokens

Product Images

Warehouse Table

Suppliers

Purchase Orders

Sales Orders

Notifications

Redis Cache

SignalR

Audit Logs

Docker Support

Azure Deployment
