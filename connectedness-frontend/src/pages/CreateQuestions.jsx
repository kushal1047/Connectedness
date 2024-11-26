import { useState } from "react";

export default function CreateQuestions() {
  const [questions, setQuestions] = useState([{ text: "", correctAnswer: "" }]);

  const AddNewQuestion = () => {
    setQuestions((prev) => [...prev, { text: "", correctAnswer: "" }]);
  };
  const handleInputChange = (index, field, value) => {
    setQuestions((prev) => {
      const updatedQuestions = [...prev];
      updatedQuestions[index] = { ...updatedQuestions[index], [field]: value };
      return updatedQuestions;
    });
  };
  const submitQuestions = (event) => {
    // TODO: Make an api call to send questions
    event.preventDefault();
    console.log("Questions submitted.", questions);
  };
  return (
    <div className="bg-gray-50 px-4 py-10 min-h-screen">
      <div className="bg-white max-w-4xl mx-auto shadow-lg rounded-2xl p-6">
        <h2 className="text-center font-bold text-2xl mb-6">
          Create Questions (Max 10)
        </h2>
        <form onSubmit={submitQuestions} className="space-y-5">
          {questions.map((question, index) => (
            <div key={index} className="bg-gray-100 p-4 rounded-lg border">
              <label className="block text-sm font-medium text-gray-700 mb-1">
                {`Question ${index + 1}`}
              </label>
              <input
                type="text"
                placeholder="Enter your question"
                value={question.text}
                onChange={(event) => {
                  handleInputChange(index, "text", event.target.value);
                }}
                className="w-full rounded-lg px-3 py-2 border mb-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
              />
              <input
                type="text"
                placeholder="Enter correct answer"
                value={question.correctAnswer}
                onChange={(event) => {
                  handleInputChange(index, "correctAnswer", event.target.value);
                }}
                className="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-green-400 mb-4"
              />
            </div>
          ))}
          <div className="flex justify-between mt-auto">
            <button
              type="button"
              onClick={AddNewQuestion}
              className="px-4 py-2 font-medium text-white hover:bg-blue-700 bg-blue-600 rounded transition duration-200"
            >
              Add New Question
            </button>
            <button
              type="submit"
              className="px-4 py-2 text-white bg-green-600 rounded-3xl font-medium hover:bg-green-700 transition duration-200"
            >
              Submit
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
