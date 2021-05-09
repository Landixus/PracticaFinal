--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2
-- Dumped by pg_dump version 13.1

-- Started on 2021-05-09 22:46:40

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3022 (class 1262 OID 24595)
-- Name: PracticaFinal; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "PracticaFinal" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Spain.1252';


ALTER DATABASE "PracticaFinal" OWNER TO postgres;

\connect "PracticaFinal"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 207 (class 1259 OID 24671)
-- Name: bloc; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bloc (
    numbloc integer NOT NULL,
    id_workout integer NOT NULL,
    pot integer NOT NULL,
    temps integer NOT NULL
);


ALTER TABLE public.bloc OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 24669)
-- Name: bloc_numbloc_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bloc_numbloc_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.bloc_numbloc_seq OWNER TO postgres;

--
-- TOC entry 3023 (class 0 OID 0)
-- Dependencies: 206
-- Name: bloc_numbloc_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bloc_numbloc_seq OWNED BY public.bloc.numbloc;


--
-- TOC entry 203 (class 1259 OID 24621)
-- Name: route; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.route (
    id_route integer NOT NULL,
    userid integer NOT NULL,
    name character varying(100) NOT NULL,
    file xml NOT NULL,
    description character varying(2000)
);


ALTER TABLE public.route OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 24619)
-- Name: route_id_route_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.route_id_route_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.route_id_route_seq OWNER TO postgres;

--
-- TOC entry 3024 (class 0 OID 0)
-- Dependencies: 202
-- Name: route_id_route_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.route_id_route_seq OWNED BY public.route.id_route;


--
-- TOC entry 201 (class 1259 OID 24598)
-- Name: user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."user" (
    id_user integer NOT NULL,
    mail character varying(50) NOT NULL,
    password character varying(50) NOT NULL,
    height integer NOT NULL,
    weight integer NOT NULL,
    maxfc integer,
    maxw integer
);


ALTER TABLE public."user" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 24596)
-- Name: user_id_user_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_id_user_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user_id_user_seq OWNER TO postgres;

--
-- TOC entry 3025 (class 0 OID 0)
-- Dependencies: 200
-- Name: user_id_user_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_id_user_seq OWNED BY public."user".id_user;


--
-- TOC entry 205 (class 1259 OID 24655)
-- Name: workout; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.workout (
    id_workout integer NOT NULL,
    id_user integer NOT NULL,
    tempstotal integer NOT NULL,
    description text,
    name character varying(200) NOT NULL
);


ALTER TABLE public.workout OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 24653)
-- Name: workout_id_workout_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.workout_id_workout_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.workout_id_workout_seq OWNER TO postgres;

--
-- TOC entry 3026 (class 0 OID 0)
-- Dependencies: 204
-- Name: workout_id_workout_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.workout_id_workout_seq OWNED BY public.workout.id_workout;


--
-- TOC entry 2873 (class 2604 OID 24674)
-- Name: bloc numbloc; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bloc ALTER COLUMN numbloc SET DEFAULT nextval('public.bloc_numbloc_seq'::regclass);


--
-- TOC entry 2871 (class 2604 OID 24624)
-- Name: route id_route; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.route ALTER COLUMN id_route SET DEFAULT nextval('public.route_id_route_seq'::regclass);


--
-- TOC entry 2870 (class 2604 OID 24601)
-- Name: user id_user; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user" ALTER COLUMN id_user SET DEFAULT nextval('public.user_id_user_seq'::regclass);


--
-- TOC entry 2872 (class 2604 OID 24658)
-- Name: workout id_workout; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workout ALTER COLUMN id_workout SET DEFAULT nextval('public.workout_id_workout_seq'::regclass);


--
-- TOC entry 2883 (class 2606 OID 24676)
-- Name: bloc bloc_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bloc
    ADD CONSTRAINT bloc_pkey PRIMARY KEY (numbloc, id_workout);


--
-- TOC entry 2879 (class 2606 OID 24629)
-- Name: route route_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.route
    ADD CONSTRAINT route_pkey PRIMARY KEY (id_route);


--
-- TOC entry 2875 (class 2606 OID 24605)
-- Name: user user_mail_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_mail_key UNIQUE (mail);


--
-- TOC entry 2877 (class 2606 OID 24603)
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id_user);


--
-- TOC entry 2881 (class 2606 OID 24663)
-- Name: workout workout_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workout
    ADD CONSTRAINT workout_pkey PRIMARY KEY (id_workout);


--
-- TOC entry 2886 (class 2606 OID 24677)
-- Name: bloc bloc_id_workout_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bloc
    ADD CONSTRAINT bloc_id_workout_fkey FOREIGN KEY (id_workout) REFERENCES public.workout(id_workout);


--
-- TOC entry 2884 (class 2606 OID 24630)
-- Name: route route_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.route
    ADD CONSTRAINT route_userid_fkey FOREIGN KEY (userid) REFERENCES public."user"(id_user);


--
-- TOC entry 2885 (class 2606 OID 24664)
-- Name: workout workout_id_user_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workout
    ADD CONSTRAINT workout_id_user_fkey FOREIGN KEY (id_user) REFERENCES public."user"(id_user);


-- Completed on 2021-05-09 22:46:40

--
-- PostgreSQL database dump complete
--

