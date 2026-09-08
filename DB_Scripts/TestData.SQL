SELECT
    b.id,
    b.title,
    b.total_count,
    b.available_count,
    c.name AS category
FROM books b
JOIN categories c
    ON b.category_id = c.id
ORDER BY b.id;


SELECT
    br.id,
    b.title,
    m.first_name || ' ' || m.last_name AS member_name,
    br.borrow_date,
    br.return_date,
    br.is_returned
FROM borrowings br
JOIN books b
    ON br.book_id = b.id
JOIN members m
    ON br.member_id = m.id
ORDER BY br.id;