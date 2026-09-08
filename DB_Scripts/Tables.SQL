CREATE TABLE categories
(
    id   INTEGER GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(100) NOT NULL,

    CONSTRAINT pk_categories
        PRIMARY KEY (id),

    CONSTRAINT uq_categories_name
        UNIQUE (name)
);


CREATE TABLE members
(
    id            INTEGER GENERATED ALWAYS AS IDENTITY,
    first_name    VARCHAR(100) NOT NULL,
    last_name     VARCHAR(100) NOT NULL,
    national_code VARCHAR(10)  NOT NULL,
    phone         VARCHAR(20),
    register_date DATE NOT NULL DEFAULT CURRENT_DATE,

    CONSTRAINT pk_members
        PRIMARY KEY (id),

    CONSTRAINT uq_members_national_code
        UNIQUE (national_code)
);


CREATE TABLE books
(
    id             INTEGER GENERATED ALWAYS AS IDENTITY,
    title          VARCHAR(200) NOT NULL,
    author         VARCHAR(150) NOT NULL,
    isbn           VARCHAR(20),
    category_id    INTEGER NOT NULL,
    total_count    INTEGER NOT NULL DEFAULT 0,
    available_count INTEGER NOT NULL DEFAULT 0,

    CONSTRAINT pk_books
        PRIMARY KEY (id),

    CONSTRAINT uq_books_isbn
        UNIQUE (isbn),

    CONSTRAINT fk_books_categories
        FOREIGN KEY (category_id)
        REFERENCES categories (id),

    CONSTRAINT chk_books_total_count
        CHECK (total_count >= 0),

    CONSTRAINT chk_books_available_count
        CHECK (available_count >= 0),

    CONSTRAINT chk_books_available_not_greater
        CHECK (available_count <= total_count)
);


CREATE TABLE borrowings
(
    id          INTEGER GENERATED ALWAYS AS IDENTITY,
    book_id     INTEGER NOT NULL,
    member_id   INTEGER NOT NULL,
    borrow_date DATE NOT NULL DEFAULT CURRENT_DATE,
    return_date DATE,
    is_returned BOOLEAN NOT NULL DEFAULT FALSE,

    CONSTRAINT pk_borrowings
        PRIMARY KEY (id),

    CONSTRAINT fk_borrowings_books
        FOREIGN KEY (book_id)
        REFERENCES books (id),

    CONSTRAINT fk_borrowings_members
        FOREIGN KEY (member_id)
        REFERENCES members (id),

    CONSTRAINT chk_borrowings_return_date
        CHECK (
            return_date IS NULL
            OR return_date >= borrow_date
        )
);