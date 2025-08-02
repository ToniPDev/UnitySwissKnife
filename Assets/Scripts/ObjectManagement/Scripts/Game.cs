using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace ObjectManagement.Scripts
{
	public class Game : PersistableObject 
	{
		const int SaveVersion = 1;

		public ShapeFactory shapeFactory;

		private InputAction _create, _load, _save, _newGame;
		
		public PersistentStorage storage;

		List<Shape> _shapes;

		void Awake () 
		{
			_shapes = new List<Shape>();
			
			Master master = new Master();
			master.Enable();

			_create = master.SaveGame.Create;
			_load = master.SaveGame.Load;
			_newGame = master.SaveGame.NewGame;
			_save = master.SaveGame.Save;
		}

		private void OnEnable()
		{
			_load.performed += Load;
			_create.performed += CreateShape;
			_save.performed += Save;
			_newGame.performed += BeginNewGame;
		}

		private void OnDisable()
		{
			_load.performed -= Load;
			_create.performed -= CreateShape;
			_save.performed -= Save;
			_newGame.performed -= BeginNewGame;
		}

		private void BeginNewGame(InputAction.CallbackContext obj) => BeginNewGame();

		private void CreateShape(InputAction.CallbackContext obj) => CreateShape();

		private void Save(InputAction.CallbackContext obj) => storage.Save(this, SaveVersion);

		private void Load(InputAction.CallbackContext obj)
		{
			BeginNewGame();
			storage.Load(this);
		}
		
		void BeginNewGame () {
			for (int i = 0; i < _shapes.Count; i++) {
				Destroy(_shapes[i].gameObject);
			}
			_shapes.Clear();
		}

		void CreateShape () 
		{
			Shape instance = shapeFactory.GetRandom();
			Transform t = instance.transform;
			t.localPosition = Random.insideUnitSphere * 5f;
			t.localRotation = Random.rotation;
			t.localScale = Vector3.one * Random.Range(0.1f, 1f);
			instance.SetColor(Random.ColorHSV(
				hueMin: 0f, hueMax: 1f,
				saturationMin: 0.5f, saturationMax: 1f,
				valueMin: 0.25f, valueMax: 1f,
				alphaMin: 1f, alphaMax: 1f
			));
			_shapes.Add(instance);
		}

		public override void Save (GameDataWriter writer) 
		{
			writer.Write(_shapes.Count);
			for (int i = 0; i < _shapes.Count; i++) 
			{
				writer.Write(_shapes[i].ShapeId);
				writer.Write(_shapes[i].MaterialId);
				_shapes[i].Save(writer);
			}
		}

		public override void Load (GameDataReader reader) 
		{
			int count = reader.ReadInt();
			for (int i = 0; i < count; i++) {
				int shapeId = reader.ReadInt();
				int materialId = reader.ReadInt();
				Shape instance = shapeFactory.Get(shapeId, materialId);
				instance.Load(reader);
				_shapes.Add(instance);
			}
		}
	}
}