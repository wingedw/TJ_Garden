import sqlalchemy as db
from sqlalchemy.sql.schema import ForeignKey

engine = db.create_engine('sqlite:///garden.db')

metadata = db.MetaData()

connection = engine.connect()

users = db.Table('Users', metadata, 
	db.Column('user_id', db.Integer, primary_key=True),
	db.Column('first_name', db.Text),
	db.Column('last_name', db.Text),
	db.Column('email_address', db.Text))

plant = db.Table('Plants', metadata, 
	db.Column('Id', db.Integer, primary_key=True),
	db.Column('Plant_Type', db.Text),
	db.Column('Common_Name', db.Text),
	db.Column('Scientific_Name', db.Text),
	db.Column('Sprout_Min', db.Integer),
	db.Column('Sprout_Max', db.Integer),
	db.Column('Mature_Time', db.Integer),
	db.Column('Spacing', db.Integer),
	db.Column('Description',db.Text))

plot = db.Table('Plots', metadata,
	db.Column('plot_id', db.Integer, primary_key=True),
	db.Column('plant_id', db.Integer),
	db.Column('schedule_id', db.Integer),
	db.Column('x_location', db.Integer),
	db.Column('y_location', db.Integer))

plot = db.Table('Schedule', metadata,
	db.Column('schedule_id', db.Integer, primary_key=True),
	db.Column('event_id', db.Integer))

metadata.create_all(engine)