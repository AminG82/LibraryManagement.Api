INSERT INTO categories (name)
VALUES
    ('Programming'),
    ('Database'),
    ('Computer Networks'),
    ('Artificial Intelligence'),
    ('Literature');


INSERT INTO members
    (first_name, last_name, national_code, phone, register_date)
VALUES
    ('علی', 'احمدی', '0012345678', '09121234567', '2026-01-10'),
    ('رضا', 'محمدی', '0023456789', '09131234567', '2026-01-15'),
    ('سارا', 'کریمی', '0034567890', '09141234567', '2026-02-01'),
    ('مریم', 'حسینی', '0045678901', '09151234567', '2026-02-10'),
    ('امیر', 'رضایی', '0056789012', '09161234567', '2026-02-18'),
    ('نگار', 'اکبری', '0067890123', '09171234567', '2026-03-02'),
    ('محمد', 'صادقی', '0078901234', '09181234567', '2026-03-12'),
    ('نیما', 'مرادی', '0089012345', '09191234567', '2026-03-20');


INSERT INTO books
    (title, author, isbn, category_id, total_count, available_count)
VALUES
    (
        'Clean Code',
        'Robert C. Martin',
        '9780132350884',
        (SELECT id FROM categories WHERE name = 'Programming'),
        5,
        4
    ),
    (
        'The Pragmatic Programmer',
        'David Thomas',
        '9780135957059',
        (SELECT id FROM categories WHERE name = 'Programming'),
        4,
        4
    ),
    (
        'C# in Depth',
        'Jon Skeet',
        '9781617294532',
        (SELECT id FROM categories WHERE name = 'Programming'),
        3,
        2
    ),
    (
        'SQL Performance Explained',
        'Markus Winand',
        '9783950307822',
        (SELECT id FROM categories WHERE name = 'Database'),
        3,
        3
    ),
    (
        'Database System Concepts',
        'Abraham Silberschatz',
        '9780078022159',
        (SELECT id FROM categories WHERE name = 'Database'),
        4,
        3
    ),
    (
        'Computer Networks',
        'Andrew S. Tanenbaum',
        '9780132126953',
        (SELECT id FROM categories WHERE name = 'Computer Networks'),
        5,
        4
    ),
    (
        'Artificial Intelligence: A Modern Approach',
        'Stuart Russell',
        '9780134610993',
        (SELECT id FROM categories WHERE name = 'Artificial Intelligence'),
        3,
        2
    ),
    (
        'Deep Learning',
        'Ian Goodfellow',
        '9780262035613',
        (SELECT id FROM categories WHERE name = 'Artificial Intelligence'),
        2,
        2
    ),
    (
        'The Little Prince',
        'Antoine de Saint-Exupery',
        '9780156012195',
        (SELECT id FROM categories WHERE name = 'Literature'),
        4,
        4
    ),
    (
        '1984',
        'George Orwell',
        '9780451524935',
        (SELECT id FROM categories WHERE name = 'Literature'),
        5,
        4
    );


INSERT INTO borrowings
    (book_id, member_id, borrow_date, return_date, is_returned)
VALUES
    (
        (SELECT id FROM books WHERE isbn = '9780132350884'),
        (SELECT id FROM members WHERE national_code = '0012345678'),
        '2026-08-01',
        NULL,
        FALSE
    ),
    (
        (SELECT id FROM books WHERE isbn = '9781617294532'),
        (SELECT id FROM members WHERE national_code = '0023456789'),
        '2026-08-05',
        NULL,
        FALSE
    ),
    (
        (SELECT id FROM books WHERE isbn = '9780078022159'),
        (SELECT id FROM members WHERE national_code = '0034567890'),
        '2026-08-10',
        NULL,
        FALSE
    ),
    (
        (SELECT id FROM books WHERE isbn = '9780134610993'),
        (SELECT id FROM members WHERE national_code = '0045678901'),
        '2026-08-12',
        NULL,
        FALSE
    ),
    (
        (SELECT id FROM books WHERE isbn = '9780451524935'),
        (SELECT id FROM members WHERE national_code = '0056789012'),
        '2026-08-15',
        NULL,
        FALSE
    ),
    (
        (SELECT id FROM books WHERE isbn = '9780132350884'),
        (SELECT id FROM members WHERE national_code = '0067890123'),
        '2026-07-01',
        '2026-07-15',
        TRUE
    ),
    (
        (SELECT id FROM books WHERE isbn = '9780135957059'),
        (SELECT id FROM members WHERE national_code = '0078901234'),
        '2026-07-10',
        '2026-07-25',
        TRUE
    ),
    (
        (SELECT id FROM books WHERE isbn = '9780156012195'),
        (SELECT id FROM members WHERE national_code = '0089012345'),
        '2026-07-20',
        '2026-08-02',
        TRUE
    );