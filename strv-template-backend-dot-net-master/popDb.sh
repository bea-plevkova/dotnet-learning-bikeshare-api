export PGPASSWORD=password
alias psqlclient="psql -h localhost -p 5432 -U postgres -d postgres -a -c "
psqlclient "WITH productCategoryId as (INSERT INTO public.\"product_categories\" (name) VALUES ('Hats') RETURNING id) INSERT INTO public.\"products\" (name, product_category_id) VALUES ('Blue Hat', (SELECT id FROM productCategoryId)),('Green Hat', (SELECT id FROM productCategoryId)),('Red Hat', (SELECT id FROM productCategoryId)),('Yellow Hat', (SELECT id FROM productCategoryId)),('Black Hat', (SELECT id FROM productCategoryId)),('White Hat', (SELECT id FROM productCategoryId));"
