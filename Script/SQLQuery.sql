USE Gymnasium;

SELECT * FROM Grading WHERE DateSet > '2025-12-01';

SELECT AVG(Grading) AS 'Average grades'
FROM Grading;
