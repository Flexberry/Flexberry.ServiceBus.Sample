-- Клиент
INSERT INTO public."Клиент"(
	primarykey, "Ид", "Наименование", description, 
	"Адрес", dnsidentity, connectionslimit, sequentialsent, 
	createtime, creator, edittime, editor)
	VALUES ('fb280f87-92a1-4f56-948b-1f7f12e38a57', 'WCFSender1', 'WCFSender1', 'WCFSender1', 
			NULL, NULL, NULL, true, 
			now(), 'admin', NULL, NULL);
			
INSERT INTO public."Клиент"(
	primarykey, "Ид", "Наименование", description, 
	"Адрес", dnsidentity, connectionslimit, sequentialsent, 
	createtime, creator, edittime, editor)
	VALUES ('5d7fd461-a69b-4916-97c6-3fd4163a5b6f', 'RESTSender1', 'RESTSender1', 'RESTSender1', 
			NULL, NULL, NULL, true, 
			now(), 'admin', NULL, NULL);
			
INSERT INTO public."Клиент"(
	primarykey, "Ид", "Наименование", description, 
	"Адрес", dnsidentity, connectionslimit, sequentialsent, 
	createtime, creator, edittime, editor)
	VALUES ('8057fe3e-9daa-4080-a85f-8cb64b1a07f1', 'Subscriber1', 'Subscriber1', 'Subscriber1', 
			NULL, NULL, NULL, true, 
			now(), 'admin', NULL, NULL);
			
INSERT INTO public."Клиент"(
	primarykey, "Ид", "Наименование", description, 
	"Адрес", dnsidentity, connectionslimit, sequentialsent, 
	createtime, creator, edittime, editor)
	VALUES ('31469c78-5265-41f8-9541-0ca87b36eeed', 'Recipient1', 'Recipient1', 'Recipient1', 
			'http://WCFListener1:5001/Listener', NULL, NULL, true, 
			now(), 'admin', NULL, NULL);

INSERT INTO public."Клиент"(
	primarykey, "Ид", "Наименование", description, 
	"Адрес", dnsidentity, connectionslimit, sequentialsent, 
	createtime, creator, edittime, editor)
	VALUES ('71883131-e9a3-4af7-9ae1-1e95f4f1c2a5', 'Recipient2', 'Recipient2', 'Recipient2', 
			NULL, NULL, NULL, true, 
			now(), 'admin', NULL, NULL);
			
-- ТипСообщения
INSERT INTO public."ТипСообщения"(
	primarykey, "Ид", "Наименование", "Комментарий", 
	createtime, creator, edittime, editor)
	VALUES ('071f592b-9e86-46a4-b652-e43fadaa0e4d', 'MessageType1', 'MessageType1', 'MessageType1', 
			now(), 'admin', NULL, NULL);
			
INSERT INTO public."ТипСообщения"(
	primarykey, "Ид", "Наименование", "Комментарий", 
	createtime, creator, edittime, editor)
	VALUES ('5d106185-5a35-40ec-b5c4-8e7ae55f3026', 'MessageType2', 'MessageType2', 'MessageType2', 
			now(), 'admin', NULL, NULL);
			
INSERT INTO public."ТипСообщения"(
	primarykey, "Ид", "Наименование", "Комментарий", 
	createtime, creator, edittime, editor)
	VALUES ('4488975e-d1cd-4a15-b15f-21ef729fac98', 'MessageType3', 'MessageType3', 'MessageType3', 
			now(), 'admin', NULL, NULL);

-- Разрешения на отправку
-- WCFSender1 - MessageType1
INSERT INTO public.outboundmessagetyperestriction(
	primarykey, "ТипСообщения", "Клиент", 
	createtime, creator, edittime, editor)
	VALUES ('7b00f9cd-e7e0-4967-b558-3e10dc348c0c', '071f592b-9e86-46a4-b652-e43fadaa0e4d', 'fb280f87-92a1-4f56-948b-1f7f12e38a57',
			now(), 'admin', NULL, NULL);
			
-- RESTSender1 - MessageType2
INSERT INTO public.outboundmessagetyperestriction(
	primarykey, "ТипСообщения", "Клиент", 
	createtime, creator, edittime, editor)
	VALUES ('d24138b9-8a56-4bf2-9211-590096efe47c', '5d106185-5a35-40ec-b5c4-8e7ae55f3026', '5d7fd461-a69b-4916-97c6-3fd4163a5b6f',
			now(), 'admin', NULL, NULL);
			
-- RESTSender1 - MessageType1
INSERT INTO public.outboundmessagetyperestriction(
	primarykey, "ТипСообщения", "Клиент", 
	createtime, creator, edittime, editor)
	VALUES ('32e31392-ac81-424b-be93-9f5aff71ac98', '071f592b-9e86-46a4-b652-e43fadaa0e4d', '5d7fd461-a69b-4916-97c6-3fd4163a5b6f',
			now(), 'admin', NULL, NULL);
			
-- Подписка
INSERT INTO public."Подписка"(
	primarykey, "Описание", expirydate, iscallback, 
	"ПередаватьПо", restrictqueuelength, maxqueuelength, 
	"ТипСообщения_m0", "Клиент_m0",
	createtime, creator, edittime, editor)
	VALUES ('d084e2e4-99c7-497d-8f06-c8412dbbb890', 'Subscriber1 - MessageType1', to_date('21000101', 'yyyyMMdd'), false, 
			'WEB', false, 1000, 
			'071f592b-9e86-46a4-b652-e43fadaa0e4d', '8057fe3e-9daa-4080-a85f-8cb64b1a07f1', 
			now(), 'admin', NULL, NULL);
			
INSERT INTO public."Подписка"(
	primarykey, "Описание", expirydate, iscallback, 
	"ПередаватьПо", restrictqueuelength, maxqueuelength, 
	"ТипСообщения_m0", "Клиент_m0",
	createtime, creator, edittime, editor)
	VALUES ('08550500-419d-4f5b-958d-c915511b98dd', 'Recipient1 - MessageType1', to_date('21000101', 'yyyyMMdd'), false, 
			'WCF', false, 1000, 
			'071f592b-9e86-46a4-b652-e43fadaa0e4d', '31469c78-5265-41f8-9541-0ca87b36eeed', 
			now(), 'admin', NULL, NULL);
			
INSERT INTO public."Подписка"(
	primarykey, "Описание", expirydate, iscallback, 
	"ПередаватьПо", restrictqueuelength, maxqueuelength, 
	"ТипСообщения_m0", "Клиент_m0",
	createtime, creator, edittime, editor)
	VALUES ('4342cc58-0eb5-4231-ab6c-9f7683ce22eb', 'Recipient2 - MessageType2', to_date('21000101', 'yyyyMMdd'), false, 
			'WCF', false, 1000, 
			'5d106185-5a35-40ec-b5c4-8e7ae55f3026', '71883131-e9a3-4af7-9ae1-1e95f4f1c2a5', 
			now(), 'admin', NULL, NULL);