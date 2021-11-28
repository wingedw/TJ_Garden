import sqlalchemy as db
from sqlalchemy.sql.schema import ForeignKey

engine = db.create_engine('sqlite:///garden.db')

metadata = db.MetaData()

connection = engine.connect()

users = db.Table('Users', metadata, 
	db.Column('User_Id', db.Integer, primary_key=True),
	db.Column('First_Name', db.Text),
	db.Column('Last_Name', db.Text),
	db.Column('Email', db.Text))

plant = db.Table('Plants', metadata, 
	db.Column('Plant_Id', db.Integer, primary_key=True),
	db.Column('Plant_Type', db.Text),
	db.Column('Common_Name', db.Text),
	db.Column('Scientific_Name', db.Text),
	db.Column('Sprout_Min', db.Integer),
	db.Column('Sprout_Max', db.Integer),
	db.Column('Mature_Time', db.Integer),
	db.Column('Spacing', db.Integer),
	db.Column('Description',db.Text))

plot = db.Table('Plots', metadata,
	db.Column('Plot_Id', db.Integer, primary_key=True),
	db.Column('Plant_Id', db.Integer),
	db.Column('x_Location', db.Integer),
	db.Column('y_Location', db.Integer))

schedule = db.Table('Schedule', metadata,
	db.Column('Schedule_Id', db.Integer, primary_key=True),
	db.Column('Plot_Id', db.Integer, foreign_key=True),
	db.Column('Event_Id', db.Integer))

event = db.Table('Events', metadata,
	db.Column('Event_Id', db.Integer, primary_key=True),
	db.Column('Name', db.Text),
	db.Column('Description', db.Text))



metadata.create_all(engine)